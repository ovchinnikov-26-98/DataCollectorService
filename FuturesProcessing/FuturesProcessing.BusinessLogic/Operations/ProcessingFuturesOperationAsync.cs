using Common.Api.Operations;
using FuturesProcessing.Api.Contract;
using FuturesProcessing.Api.Item;
using FuturesProcessing.Api.Services;

namespace FuturesProcessing.BusinessLogic.Operations
{
    public class ProcessingFuturesOperationAsync : IOperationAsync<FuturesContract, PriceProcessedItem>
    {
        private readonly IFuturesManipulationsService _jobService;

        public ProcessingFuturesOperationAsync(IFuturesManipulationsService jobService)
        {
            ArgumentNullException.ThrowIfNull(nameof(jobService));

            _jobService = jobService;
        }

        public async Task<Result<PriceProcessedItem>> ExecuteAsync(FuturesContract contract)
        {
            var first = await _jobService.GetDataAsync(new FuturesContract() { Symbol = "BTCUSDT_250627" });
            var second = await _jobService.GetDataAsync(new FuturesContract() { Symbol = "BTCUSDT_250926" });

            var result = second.Data.Price - first.Data.Price;
            throw new NotImplementedException();
        }
    }
}
