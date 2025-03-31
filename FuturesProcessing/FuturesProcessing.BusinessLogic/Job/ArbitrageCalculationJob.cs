using Common.Api.Operations;
using FuturesProcessing.Api.Contract;
using FuturesProcessing.Api.Item;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuturesProcessing.BusinessLogic.Job
{
    [DisallowConcurrentExecution]
    public class ArbitrageCalculationJob : IJob
    {
        private readonly IOperationAsync<FuturesCoupleContract, ArbitrageResultItem> _arbitrageCalculator;
        private readonly ILogger<ArbitrageCalculationJob> _logger;

        public ArbitrageCalculationJob(
            IOperationAsync<FuturesCoupleContract, ArbitrageResultItem> arbitrageCalculator,
            ILogger<ArbitrageCalculationJob> logger)
        {
            ArgumentNullException.ThrowIfNull(nameof(arbitrageCalculator));
            ArgumentNullException.ThrowIfNull(nameof(logger));

            _arbitrageCalculator = arbitrageCalculator;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var jobData = context.JobDetail.JobDataMap;
            var firstSymbol = jobData.GetString("firstFutureSymbol") ?? "BTCUSDT_QUARTER";
            var secondSymbol = jobData.GetString("secondFutureSymbol") ?? "BTCUSDT_BI-QUARTER";

            try
            {
                _logger.LogInformation(
                    "Starting arbitrage calculation job for {First} and {Second} at {Time}",
                    firstSymbol, secondSymbol, DateTime.UtcNow);

                var result = await _arbitrageCalculator.ExecuteAsync(new FuturesCoupleContract() { FirstFutureSymbol = firstSymbol, SecondFutureSymbol = secondSymbol});

                _logger.LogInformation(
                    "Arbitrage calculation completed. Difference: {Difference}",
                    result.Data.Difference);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error in arbitrage calculation job for {First} and {Second}",
                    firstSymbol, secondSymbol);
                throw new JobExecutionException(ex, false);
            }
        }
    }
}
