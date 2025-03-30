using Common.WebApi.Options;
using Microsoft.AspNetCore.Http;

namespace Common.WebApi.Logging
{
    internal sealed class DefaultRequestLogMessageGenerator : IRequestLogMessageGenerator
    {
        private static readonly string StructureLogFormat =
                "Headers: '{headers}'; requested address:'{requestedAddress}'; method:'{method}'; " +
                "request from: '{ipFrom}'; requested server: {ipTo}; " +
                $"requested by user: {{userName}};{Environment.NewLine}request body:{Environment.NewLine}'{{body}}'; ";

        public LogMessage Create(HttpContext httpContext, RequestResponseLoggingOptions? options)
        {
            var parameters = new object?[]
            {
                httpContext.GetRequestHeaders(),
                httpContext.GetRequesUri()?.ToString(),
                httpContext.Request.Method,
                httpContext.Connection.RemoteIpAddress?.ToString() ?? "(null)",
                httpContext.Connection.LocalIpAddress?.ToString() ?? "(null)",
                httpContext.User?.Identity?.Name ?? "(null)",
                httpContext.GetRequestBody(),
            };

            return new LogMessage(StructureLogFormat, parameters);
        }
    }
}


