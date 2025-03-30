using Common.Api.Operations;
using DataCollector.Api.Contract;
using DataCollector.Api.Item;

namespace DataCollector.Api.Services
{
    public interface IFuturesService
    {
        public Task<Result<FuturesPriceItem>> GetFuturesPricesAsync(FuturesContract contract);
    }
}
