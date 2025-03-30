using Common.WebApi.Options;
using Microsoft.AspNetCore.Http;

namespace Common.WebApi.Logging
{
    /// <summary>
    /// Request log message generator.
    /// </summary>
    public interface IRequestLogMessageGenerator
    {
        /// <summary>
        /// Creates request log message.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="options"></param>
        LogMessage Create(HttpContext httpContext, RequestResponseLoggingOptions? options);
    }

}
