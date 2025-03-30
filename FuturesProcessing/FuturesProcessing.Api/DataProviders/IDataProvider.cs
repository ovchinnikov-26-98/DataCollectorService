using Common.Api.Operations;
using FuturesProcessing.Api.Contract;
using FuturesProcessing.Api.Item;

namespace FuturesProcessing.Api.DataProviders
{
    public interface IDataProvider
    {
        Task<Result<FuturesPriceItem>> FetchDataAsync(FuturesContract contract);
    }
}
