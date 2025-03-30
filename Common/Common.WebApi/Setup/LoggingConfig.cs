using Common.WebApi.Logging;
using Common.WebApi.Middlewares;
using Common.WebApi.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Common.WebApi.Setup
{

    /// <summary>
    /// Logging configuration.
    /// </summary>
    public static class LoggingConfig
    {
        /// <summary>
        /// Configures logging.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureLogging(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.Configure<RequestResponseLoggingOptions>(configuration.GetSection("Logging"));
            services.TryAddSingleton<IRequestLogMessageGenerator, DefaultRequestLogMessageGenerator>();
            services.TryAddSingleton<IResponseLogMessageGenerator, DefaultResponseLogMessageGenerator>();
        }

        /// <summary>
        /// Use configured logging.
        /// </summary>
        /// <param name="app"></param>
        public static IApplicationBuilder UseConfiguredLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }

}
