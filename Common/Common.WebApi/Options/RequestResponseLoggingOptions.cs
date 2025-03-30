namespace Common.WebApi.Options
{
    /// <summary>
    /// RequestResponseLoggingOptionsMiddleware options.
    /// </summary>
    public class RequestResponseLoggingOptions
    {
        /// <summary>
        /// Lof request required.
        /// </summary>
        public bool IsLogRequest {  get; set; }

        /// <summary>
        /// Lof Response required.
        /// </summary>
        public bool IsLogResponse {  get; set; }

        /// <summary>
        /// Lof Response body required.
        /// </summary>
        public bool IsLogResponseBody {  get; set; }
    }
}
