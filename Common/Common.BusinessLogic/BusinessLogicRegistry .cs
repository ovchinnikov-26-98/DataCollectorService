using Common.Api.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Common.BusinessLogic
{
    /// <summary>
    /// Business logic registry.
    /// </summary>
    public class BusinessLogicRegistry : RegistryBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public BusinessLogicRegistry(IServiceCollection serviceCollection)
            : base(serviceCollection)
        {

        }
    }
}

