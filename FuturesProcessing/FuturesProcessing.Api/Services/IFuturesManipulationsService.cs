using Common.Api.Operations;
using FuturesProcessing.Api.Contract;
using FuturesProcessing.Api.Item;

namespace FuturesProcessing.Api.Services
{
    public interface IFuturesManipulationsService
    {
        public Task<Result<FuturesPriceItem>> GetDataAsync(FuturesContract contract);

        public Task<Result<ArbitrageResultItem>> ProcessingFuturesAsync(FuturesCoupleContract contract);
    }
}
