using Common.Api.Operations;
using FuturesProcessing.Api.Contract;
using FuturesProcessing.Api.Item;
using FuturesProcessing.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuturesProcessing.WebApi.Controllers
{
    [Route("api/v1/JobFutures")]
    [ApiController]
    public class JobFuturesController : ControllerBase
    {
        private readonly IFuturesManipulationsService _jobService;

        public JobFuturesController(IFuturesManipulationsService jobService)
        {
            ArgumentNullException.ThrowIfNull(nameof(jobService));

            _jobService = jobService;
        }

        [HttpGet("StartJob")]
        [ProducesResponseType(typeof(Result<PriceProcessedItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<PriceProcessedItem>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<PriceProcessedItem>> StartJob([FromQuery] FuturesContract contract)
        {
            var ree = await _jobService.ProcessingFuturesAsync(contract);
            return new Result<PriceProcessedItem>();
        }
    }
}
