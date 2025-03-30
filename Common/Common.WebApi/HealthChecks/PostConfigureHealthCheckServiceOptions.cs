using Common.Data.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Common.WebApi.HealthChecks
{
    /// <summary>
    /// HealthCheckServiceOptions post configure.
    /// </summary>
    public class PostConfigureHealthCheckServiceOptions : PostConfigureOptions<
                                                                HealthCheckServiceOptions,
                                                                IEnumerable<IOptionsChangeTokenSource<ConnectionStringOptions>>,
                                                                IOptionsMonitor<ConnectionStringOptions>>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="optionsMonitor"></param>
        public PostConfigureHealthCheckServiceOptions(
                IEnumerable<IOptionsChangeTokenSource<ConnectionStringOptions>> sources,
                IOptionsMonitor<ConnectionStringOptions> optionsMonitor)
            : base(Microsoft.Extensions.Options.Options.DefaultName, sources, optionsMonitor, PostConfigureOptions)
        {
        }

        private static void PostConfigureOptions(
            HealthCheckServiceOptions healthCheckServiceOptions,
            IEnumerable<IOptionsChangeTokenSource<ConnectionStringOptions>> sources,
            IOptionsMonitor<ConnectionStringOptions> optionsMonitor)
        {
            if (null == sources)
            {
                return;
            }

            var sourcesArray = sources.ToArray();
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
            var tags = new[] { "db" };

            for (var i = 0; i < sourcesArray.Length; i++)
            {
                var optionName = sourcesArray[i].Name;

                var options = string.IsNullOrWhiteSpace(optionName)
                                ? optionsMonitor.CurrentValue
                                : optionsMonitor.Get(optionName);

                //if (!options.AddHealthCheck ||
                //    string.IsNullOrWhiteSpace(options.ConnectionString))
                //{
                //    continue;
                //}

                string checkName;

                if (string.IsNullOrWhiteSpace(optionName))
                {
                    checkName = "Default";
                }
                else
                {
                    checkName = optionName.Replace("ConnectionString", "");
                }

                var args = new object?[] { optionName, version };

                var registration = new HealthCheckRegistration(checkName, s => ActivatorUtilities.CreateInstance<DbHealthCheck>(s, args), null, tags);

                healthCheckServiceOptions.Registrations.Add(registration);
            }
        }
    }

}
