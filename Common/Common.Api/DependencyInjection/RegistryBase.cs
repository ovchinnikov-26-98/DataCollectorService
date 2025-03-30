using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.DependencyInjection
{
    /// <summary>
    /// Base class for registering dependencies.
    /// </summary>
    public abstract class RegistryBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceCollection"></param>
        protected RegistryBase(IServiceCollection serviceCollection)
        {
            ArgumentNullException.ThrowIfNull(serviceCollection);
        }
    }

}
