using System.Data.Common;

namespace Common.Data.ConnectionManager
{
    /// <summary>
    /// Db connection manager.
    /// </summary>
    public interface IConnectionManager
    {
        /// <summary>
        /// Creates new connection.
        /// </summary>
        /// <param name="name"></param>
        DbConnection Connect();

        /// <summary>
        /// Resolves connection.
        /// </summary>
        DbConnection ResolveConnection(DbConnection connection);

        /// <summary>
        /// Resolves transaction.
        /// </summary>
        DbTransaction ResolveTransaction(DbTransaction transaction);
    }

}
