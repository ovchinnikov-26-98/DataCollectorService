using Common.WebApi.Options;
using Microsoft.AspNetCore.Http;

namespace Common.WebApi.Logging
{
    /// <summary>
    /// Response log message generator.
    /// </summary>
    public interface IResponseLogMessageGenerator
    {
        /// <summary>
        /// Creates response log message.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="options"></param>
        LogMessage Create(HttpContext httpContext, RequestResponseLoggingOptions? options);
    }

}
