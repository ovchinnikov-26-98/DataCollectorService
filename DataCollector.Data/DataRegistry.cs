using Common.Api.DependencyInjection;
using DataCollector.Api.Repositories;
using DataCollector.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace DataCollector.Data
{

    /// <summary>
    /// Data Registry
    /// </summary>
    public class DataRegistry : RegistryBase
    {
        public DataRegistry(IServiceCollection serviceCollection) : base(serviceCollection)
        {

            _ = serviceCollection.AddSingleton<IFuturesRepository, FuturesRepository>();
        }
    }
}
