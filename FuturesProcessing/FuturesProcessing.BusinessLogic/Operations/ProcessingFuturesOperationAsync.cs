using Common.Api.Operations;
using FuturesProcessing.Api.Contract;
using FuturesProcessing.Api.Item;
using FuturesProcessing.Api.Repositories;
using FuturesProcessing.Api.Services;

namespace FuturesProcessing.BusinessLogic.Operations
{
    public class ProcessingFuturesOperationAsync : IOperationAsync<FuturesCoupleContract, ArbitrageResultItem>
    {
        private readonly IFuturesManipulationsService _jobService;
        private readonly IArbitrageRepository _arbitrageRepository;

        public ProcessingFuturesOperationAsync(IFuturesManipulationsService jobService, IArbitrageRepository arbitrageRepository)
        {
            ArgumentNullException.ThrowIfNull(nameof(jobService));
            ArgumentNullException.ThrowIfNull(nameof(arbitrageRepository));

            _jobService = jobService;
            _arbitrageRepository = arbitrageRepository;
        }

        public async Task<Result<ArbitrageResultItem>> ExecuteAsync(FuturesCoupleContract contract)
        {
            var first = await _jobService.GetDataAsync(new FuturesContract() { Symbol = contract.FirstFutureSymbol});
            var second = await _jobService.GetDataAsync(new FuturesContract() { Symbol = contract.SecondFutureSymbol });

            var difference = first.Data.Price - second.Data.Price;

            var result = new ArbitrageResultItem
            {
                Time = first.Data.Time,
                Difference = difference,
                BiQuarterPrice = second.Data.Price,
                QuarterPrice = first.Data.Price,
                BiQuarterSymbol = second.Data.Symbol,
                QuarterSymbol = first.Data.Symbol
            };

            await _arbitrageRepository.SaveArbitrageResultAsync(result);

            return new Result<ArbitrageResultItem>() { Data = result };
        }
    }
}
