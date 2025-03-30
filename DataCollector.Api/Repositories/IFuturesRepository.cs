using DataCollector.Api.Item;

namespace DataCollector.Api.Repositories
{
    public interface IFuturesRepository
    {
        public Task<FuturesPriceItem> GetLatestAsync(string symbol);

        public Task AddAsync(FuturesPriceItem data);
    }
}
