using Common.Data.ConnectionManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data.Common;

namespace Common.WebApi.HealthChecks
{
    /// <summary>
    /// Database health check.
    /// </summary>
    public class DbHealthCheck : IHealthCheck
    {

        private readonly string? _version;

        private readonly string? _connectionName;

        private readonly IConnectionManager _connectionManager;



        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionManager"></param>
        /// <param name="connectionName"></param>
        /// <param name="version"></param>
        public DbHealthCheck(IConnectionManager connectionManager, string? connectionName, string? version)
        {
            ArgumentNullException.ThrowIfNull(connectionManager);

            _connectionManager = connectionManager;
            _connectionName = connectionName;
            _version = version;

        }



        /// <inheritdoc />

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var status = HealthStatus.Unhealthy;

            DbConnection? connection = null;

            try
            {
                connection = _connectionManager.Connect();

                await connection.OpenAsync(cancellationToken)
                                       .ConfigureAwait(false);

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT 1";

                    _ = await command.ExecuteNonQueryAsync(cancellationToken)
                                           .ConfigureAwait(false);

                }

                status = HealthStatus.Healthy;

            }
            catch
            {

                // No action required.

            }



            if (System.Data.ConnectionState.Closed < connection?.State)
            {
                _ = connection.CloseAsync();
            }



            var data = new Dictionary<string, object>
            {
                  { "Version", _version ?? "undefined" }
            };

            return new HealthCheckResult(status, data: data);

        }

    }
}
