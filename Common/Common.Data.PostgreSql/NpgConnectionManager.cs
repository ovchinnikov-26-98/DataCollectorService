using Common.Data.ConnectionManager;
using Common.Data.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data.Common;

namespace Common.Data.PostgreSql
{
    /// <summary>
    /// Connection manager for MySql and MariaDb.
    /// </summary>
    public class NpgConnectionManager : IConnectionManager
    {
        private readonly IOptionsMonitor<ConnectionStringOptions> _connectionStringsOptionsMonitor;
        private readonly IConfiguration _configuration1;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionStringsOptionsMonitor"></param>
        public NpgConnectionManager(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            _configuration1 = configuration;
        }

        /// <inheritdoc />
        public DbConnection Connect()
        {
            var connectionString = _configuration1["ConnectionStrings:PostgresSql:ConnectionString"];

            var connection = new NpgsqlConnection(connectionString);

            return connection;
        }

        /// <inheritdoc />
        public DbConnection ResolveConnection(DbConnection connection)
        {
            return connection;
        }

        /// <inheritdoc />
        public DbTransaction ResolveTransaction(DbTransaction transaction)
        {
            return transaction;
        }
    }

}
