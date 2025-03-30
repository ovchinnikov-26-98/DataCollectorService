using Common.Api.DependencyInjection;
using Common.Api.Operations;
using Common.Api.Services;
using Common.Api.Validation;
using FuturesProcessing.Api.Contract;
using FuturesProcessing.Api.DataProviders;
using FuturesProcessing.Api.Item;
using FuturesProcessing.Api.Services;
using FuturesProcessing.BusinessLogic.DataProviders;
using FuturesProcessing.BusinessLogic.Operations;
using FuturesProcessing.BusinessLogic.Services;
using FuturesProcessing.BusinessLogic.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace FuturesProcessing.BusinessLogic
{
    public class BusinessLogicRegistry : RegistryBase
    {
        public BusinessLogicRegistry(IServiceCollection serviceCollection) : base(serviceCollection)
        {

            _ = serviceCollection.AddTransient<IDataProvider, BinanceDataProvider>();
            _ = serviceCollection.AddTransient<IFuturesManipulationsService, FuturesManipulationsService>();

            _ = serviceCollection.AddTransient<IOperationAsync<FuturesContract, PriceProcessedItem>, ProcessingFuturesOperationAsync>();
            _ = serviceCollection.AddTransient<IOperationAsync<FuturesContract, FuturesPriceItem>, GetDataOperation>();

            _ = serviceCollection.AddTransient<IValidatorAsync<FuturesContract>, FuturesContractValidator>();
        }

    }
}
