using Common.Api.Operations;
using FuturesProcessing.Api.Contract;
using FuturesProcessing.Api.Item;
using FuturesProcessing.Api.Services;
using FuturesProcessing.BusinessLogic.Job;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quartz;

namespace FuturesProcessing.WebApi.Controllers
{
    [Route("api/v1/JobFutures")]
    [ApiController]
    public class JobFuturesController : ControllerBase
    {
        private readonly IFuturesManipulationsService _futuresManipulationsService;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly ILogger<JobFuturesController> _logger;


        public JobFuturesController(IFuturesManipulationsService futuresManipulationsService, ISchedulerFactory schedulerFactory,
        ILogger<JobFuturesController> logger)
        {
            ArgumentNullException.ThrowIfNull(nameof(futuresManipulationsService));
            ArgumentNullException.ThrowIfNull(nameof(schedulerFactory));
            ArgumentNullException.ThrowIfNull(nameof(logger));

            _futuresManipulationsService = futuresManipulationsService;
            _schedulerFactory = schedulerFactory;
            _logger = logger;
        }

        /// <summary>
        /// Запуск расчета арбитража для указанных фьючерсов
        /// </summary>
        [HttpPost("calculate")]
        [ProducesResponseType(typeof(Result<FuturesPriceItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<FuturesPriceItem>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<ArbitrageResultItem>> CalculateArbitrage([FromBody] FuturesCoupleContract contract)
        {
            var result = await _futuresManipulationsService.ProcessingFuturesAsync(contract);

            return result;
        }

        /// <summary>
        /// Создание периодического задания для расчета арбитража
        /// </summary>
        [HttpPost("schedule")]
        public async Task<IActionResult> ScheduleArbitrageJob([FromBody] ScheduleArbitrageContract request)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            var jobKey = new JobKey($"ArbitrageJob_{request.FirstFutureSymbol}_{request.SecondFutureSymbol}");
            var triggerKey = new TriggerKey($"ArbitrageTrigger_{request.FirstFutureSymbol}_{request.SecondFutureSymbol}");

            var job = JobBuilder.Create<ArbitrageCalculationJob>()
                .WithIdentity(jobKey)
                .UsingJobData("firstFutureSymbol", request.FirstFutureSymbol)
                .UsingJobData("secondFutureSymbol", request.SecondFutureSymbol)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .WithCronSchedule(request.CronExpression)
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            _logger.LogInformation(
                "Scheduled arbitrage job for {First} and {Second} with cron {Cron}",
                request.FirstFutureSymbol, request.SecondFutureSymbol, request.CronExpression);

            return Ok(new { JobId = jobKey.Name });
        }
       
    }
}
