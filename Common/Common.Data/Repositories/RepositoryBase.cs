using Common.Data.ConnectionManager;
using System.Data.Common;

namespace Common.Data.Repositories
{
    /// <summary>
    /// Base class for repository.
    /// </summary>
    public abstract class RepositoryBase
    {
        private readonly IConnectionManager _connectionManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionManager"></param>
        protected RepositoryBase(IConnectionManager connectionManager)
        {
            ArgumentNullException.ThrowIfNull(connectionManager);

            _connectionManager = connectionManager;
        }

        /// <summary>
        /// Current connection.
        /// </summary>
        protected DbConnection Connection => _connectionManager.Connect();
    }

}
