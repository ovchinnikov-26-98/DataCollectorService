using Common.Api.Execution;
using Common.Api.Operations;
using Common.Api.Services;
using FuturesProcessing.Api.Contract;
using FuturesProcessing.Api.Item;
using FuturesProcessing.Api.Services;

namespace FuturesProcessing.BusinessLogic.Services
{
    public class FuturesManipulationsService : ServiceBase, IFuturesManipulationsService
    {
        public FuturesManipulationsService(IMediator mediator) : base(mediator)
        {
        }

        public async Task<Result<FuturesPriceItem>> GetDataAsync(FuturesContract contract)
        {
            return await ExecuteContractAsync<FuturesContract, FuturesPriceItem>(contract);
        }

        public async Task<Result<PriceProcessedItem>> ProcessingFuturesAsync(FuturesContract contract)
        {
            return await ExecuteContractAsync<FuturesContract, PriceProcessedItem>(contract);
        }
    }
}
