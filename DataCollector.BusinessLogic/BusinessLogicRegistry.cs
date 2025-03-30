using Common.Api.DependencyInjection;
using Common.Api.Operations;
using Common.Api.Validation;
using DataCollector.Api.Contract;
using DataCollector.Api.Item;
using DataCollector.Api.Services;
using DataCollector.BusinessLogic.Operations;
using DataCollector.BusinessLogic.Services;
using DataCollector.BusinessLogic.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace DataCollector.BusinessLogic
{
    /// <summary>
    /// Business Logic Registry
    /// </summary>
    public class BusinessLogicRegistry : RegistryBase
    {
        public BusinessLogicRegistry(IServiceCollection serviceCollection) : base(serviceCollection)
        {
            _ = serviceCollection.AddTransient<IOperationAsync<FuturesContract, FuturesPriceItem>, GetFuturesPricesOperationAsync>();
            _ = serviceCollection.AddTransient<IFuturesService, FuturesService>();
            _ = serviceCollection.AddTransient<IValidatorAsync<FuturesContract>, FuturesContractValidatorAsync>();
        }
    }
}
