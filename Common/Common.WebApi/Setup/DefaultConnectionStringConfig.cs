using Common.Data.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.WebApi.Setup
{
    /// <summary>
    /// Default connection string configuration.
    /// </summary>
    public static class DefaultConnectionStringConfig
    {
        /// <summary>
        /// DefaultConnectionString section name.
        /// </summary>
        public const string SectionName = "DefaultConnectionString";

        /// <summary>
        /// Configures default connection string.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection ConfigureConnectionStrings(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.Configure<ConnectionStringOptions>(configuration.GetSection(SectionName));

            return services;
        }

    }
}
