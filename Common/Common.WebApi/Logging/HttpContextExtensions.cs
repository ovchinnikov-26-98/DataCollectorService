using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Common.WebApi.Logging
{
    /// <summary>
    /// <see cref="HttpContext" /> extension methods.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Gets request Uri.
        /// </summary>
        /// <param name="httpContext"></param>
        public static Uri GetRequesUri(this HttpContext httpContext)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = httpContext.Request.Scheme,
                Host = httpContext.Request.Host.Host,
                Path = httpContext.Request.Path.ToString(),
                Query = httpContext.Request.QueryString.ToString()
            };

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Gets request body as UTF-8 string.
        /// </summary>
        /// <param name="httpContext"></param>
        public static string GetRequestBody(this HttpContext httpContext)
        {
            if (!httpContext.Request.ContentLength.HasValue ||
                0 == httpContext.Request.ContentLength)
            {
                return string.Empty;
            }

            if (!httpContext.Request.Body.CanRead)
            {
                return "(null)";
            }

            string body;

            if (!httpContext.Request.Body.CanSeek)
            {
                var bodyStream = new MemoryStream((int)httpContext.Request.ContentLength.Value);

                httpContext.Request.Body.CopyToAsync(bodyStream)
                                            .GetAwaiter()
                                            .GetResult();

                httpContext.Request.Body = bodyStream;

                bodyStream.Position = 0;

                body = HttpUtility.UrlDecode(new StreamReader(bodyStream).ReadToEnd(), Encoding.UTF8);

                bodyStream.Position = 0;
            }
            else
            {
                httpContext.Request.Body.Position = 0;

                var rawBody = new StreamReader(httpContext.Request.Body)
                                .ReadToEndAsync()
                                .GetAwaiter()
                                .GetResult();
                body = HttpUtility.UrlDecode(rawBody, Encoding.UTF8);

                httpContext.Request.Body.Position = 0;
            }

            return body;
        }

        /// <summary>
        /// Gets request headers.
        /// </summary>
        /// <param name="httpContext"></param>
        public static string? GetRequestHeaders(this HttpContext httpContext)
        {
            var headers = httpContext.Request.Headers?.Aggregate(new StringBuilder(), (s, c) => s.Append($"{c.Key}={c.Value};"));

            return headers?.ToString();
        }

        /// <summary>
        /// Gets response body as UTF-8 string.
        /// </summary>
        /// <param name="httpContext"></param>
        public static string GetResponseBody(this HttpContext httpContext)
        {
            if (!httpContext.Response.Body.CanRead)
            {
                return "(null)";
            }

            if (0 == httpContext.Response.Body.Length)
            {
                return string.Empty;
            }

            if (httpContext.Response.Body.CanSeek)
            {
                httpContext.Response.Body.Position = 0;
            }

            var body = HttpUtility.UrlDecode(new StreamReader(httpContext.Response.Body).ReadToEnd(), Encoding.UTF8);

            if (httpContext.Response.Body.CanSeek)
            {
                httpContext.Response.Body.Position = 0;
            }

            return body;
        }

        /// <summary>
        /// Gets response headers.
        /// </summary>
        /// <param name="httpContext"></param>
        public static string? GetResponseHeaders(this HttpContext httpContext)
        {
            var headers = httpContext.Response.Headers?.Aggregate(new StringBuilder(), (s, c) => s.Append($"{c.Key}={c.Value};"));

            return headers?.ToString();
        }

    }
}
