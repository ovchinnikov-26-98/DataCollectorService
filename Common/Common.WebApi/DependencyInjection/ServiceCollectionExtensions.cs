using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.WebApi.DependencyInjection
{
    /// <summary>
	/// Dependency injection <see cref="IServiceCollection" /> extensions.
	/// </summary>
	public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Authentication.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            //РЕализовать, если необходима аунтефикация 
            return services;
        }

        /// <summary>
        /// Adds client certificate.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public static IHttpClientBuilder AddConfiguredHttpClient(this IServiceCollection services, IConfigurationSection section)
        {
            var name = section.Key;

            return services.AddHttpClient(
                                $"{name}HttpClient",
                                client =>
                                {
                                    client.BaseAddress = section.GetSection("Endpoint")
                                                                .Get<Uri>();
                                });
        }

    }
}
