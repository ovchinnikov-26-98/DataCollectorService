using FuturesProcessing.Api.Item;

namespace FuturesProcessing.Api.Repositories
{
    public interface IArbitrageRepository
    {
        Task SaveArbitrageResultAsync(ArbitrageResultItem result);
    }
}
