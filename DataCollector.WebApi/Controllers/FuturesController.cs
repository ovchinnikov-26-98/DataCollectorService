using Common.Api.Operations;
using DataCollector.Api.Contract;
using DataCollector.Api.Item;
using DataCollector.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataCollector.WebApi.Controllers
{
    /// <summary>
    /// Futures
    /// </summary>
    [Route("api/v1/Futures")]
    [ApiController]
    public class FuturesController : ControllerBase
    {
        private readonly IFuturesService _futuresService;

        public FuturesController(IFuturesService futuresService)
        {
            ArgumentNullException.ThrowIfNull(nameof(futuresService));

            _futuresService = futuresService;
        }

        [HttpGet("GetPrices")]
        [ProducesResponseType(typeof(Result<FuturesPriceItem>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<FuturesPriceItem>),StatusCodes.Status500InternalServerError)]
        public async Task<Result<FuturesPriceItem>> GetPrices([FromQuery] FuturesContract contract)
        {
            var prices = await  _futuresService.GetFuturesPricesAsync(contract);
            return prices;
        }
    }
}
