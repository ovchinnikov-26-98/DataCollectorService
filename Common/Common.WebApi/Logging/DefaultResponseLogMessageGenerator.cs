using Common.Api.Logging;
using Common.WebApi.Options;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Common.WebApi.Logging
{
    internal sealed class DefaultResponseLogMessageGenerator : IResponseLogMessageGenerator
    {
        private const string StructureLogFormat = "Headers: '{headers}'; result code: {resultCode};";
        private static readonly string FullStructureLogFormat =
                                        $"Headers: '{{headers}}'; result code: {{resultCode}};{Environment.NewLine}response body:{Environment.NewLine}'{{body}}';";
        public LogMessage Create(HttpContext httpContext, RequestResponseLoggingOptions? options)
        {
            object?[] parameters;

            if (true == options?.IsLogResponseBody)
            {
                parameters =
                [
                    httpContext.GetResponseHeaders(),
                httpContext.Response.StatusCode,
                httpContext.GetResponseBody()
                ];

                return new LogMessage(FullStructureLogFormat, parameters);
            }

            parameters =
            [
                httpContext.GetResponseHeaders(),
            httpContext.Response.StatusCode
            ];

            return new LogMessage(StructureLogFormat, parameters);
        }
    }

}
