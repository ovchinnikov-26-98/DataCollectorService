namespace Common.Data.Options
{
    /// <summary>
    /// Connection string options.
    /// </summary>
    public class ConnectionStringOptions
    {
        /// <summary>
        /// Connection string value.
        /// </summary
        public string? ConnectionString { get; set; }

        /// <summary>
        /// Add Db health check for this connection. By default is set to true.
        /// </summary>
        public bool AddHealthCheck { get; set; } = true;
    }

}
