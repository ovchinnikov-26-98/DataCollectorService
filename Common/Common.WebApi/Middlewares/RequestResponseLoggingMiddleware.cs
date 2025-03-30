using Common.WebApi.Logging;
using Common.WebApi.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common.WebApi.Middlewares
{
    /// <summary>
	/// Middleware that performs logging of incoming and outcoming requests.
	/// </summary>
	public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IRequestLogMessageGenerator _requestLogMessageGenerator;
        private readonly IResponseLogMessageGenerator _responseLogMessageGenerator;
        private readonly IOptionsMonitor<RequestResponseLoggingOptions> _optionsMonitor;

        /// <summary>
        /// Creates logging middleware.
        /// </summary>
        /// <param name="next">Next middleware to be used.</param>
        /// <param name="requestLogMessageGenerator"></param>
        /// <param name="responseLogMessageGenerator"></param>
        /// <param name="logger"></param>
        /// <param name="optionsMonitor"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public RequestResponseLoggingMiddleware(
            RequestDelegate next,
            IRequestLogMessageGenerator requestLogMessageGenerator,
            IResponseLogMessageGenerator responseLogMessageGenerator,
            ILogger<RequestResponseLoggingMiddleware> logger,
            IOptionsMonitor<RequestResponseLoggingOptions> optionsMonitor
        )
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _requestLogMessageGenerator = requestLogMessageGenerator ?? throw new ArgumentNullException(nameof(requestLogMessageGenerator));
            _responseLogMessageGenerator = responseLogMessageGenerator ?? throw new ArgumentNullException(nameof(responseLogMessageGenerator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _optionsMonitor = optionsMonitor ?? throw new ArgumentNullException(nameof(optionsMonitor));
        }

        /// <inheritdoc />
        public async Task Invoke(HttpContext context)
        {
            var options = _optionsMonitor.Get(null);
            if (true == options?.IsLogRequest)
            {
                var logMessage = _requestLogMessageGenerator.Create(context, options);

                if (null == logMessage)
                {
                    return;
                }
                _logger.LogTrace(logMessage.Format, logMessage.Parameters);
            }

            if (true == options?.IsLogResponse &&
                true == options?.IsLogResponseBody)
            {
                // Copy a pointer to the original response body stream.
                var originalBodyStream = context.Response.Body;

                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    await _next.Invoke(context)
                            .ConfigureAwait(false);

                    LogResponse(context, options);

                    responseBody.Position = 0;

                    // Copy the contents of the memory stream (which contains the response) to the original stream,
                    // which is then returned to the client.
                    await responseBody.CopyToAsync(originalBodyStream)
                        .ConfigureAwait(false);
                }
            }
            else if (true == options?.IsLogResponse)
            {
                await _next.Invoke(context)
                    .ConfigureAwait(false);

                LogResponse(context, options);
            }
            else
            {
                await _next.Invoke(context)
                    .ConfigureAwait(false);
            }
        }

        private void LogResponse(HttpContext context, RequestResponseLoggingOptions options)
        {
            var logMessage = _responseLogMessageGenerator.Create(context, options);

            if (null == logMessage)
            {
                return;
            }

            _logger.LogTrace(logMessage.Format, logMessage.Parameters);

        }
    }

}
