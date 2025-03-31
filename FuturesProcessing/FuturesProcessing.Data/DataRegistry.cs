using Common.Api.DependencyInjection;
using FuturesProcessing.Api.Repositories;
using FuturesProcessing.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FuturesProcessing.Data
{
    /// <summary>
    /// Data Registry
    /// </summary>
    public class DataRegistry : RegistryBase
    {
        public DataRegistry(IServiceCollection serviceCollection) : base(serviceCollection)
        {

            _ = serviceCollection.AddSingleton<IArbitrageRepository, ArbitrageRepository>();
        }
    }
}
