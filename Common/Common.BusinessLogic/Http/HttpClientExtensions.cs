using Common.Api.Operations;
using System.Net.Http.Json;
using System.Text.Json;

namespace Common.BusinessLogic.Http
{
    /// <summary>
    /// Http client extensions methods.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Sends http POST request.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="uri"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static Result<TResult> SendPost<TResult>(this HttpClient httpClient, string uri, IDictionary<string, object>? body)
            where TResult : class
        {
            using (var message = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                if (null != body)
                {
                    message.Content = JsonContent.Create(body);
                }

                try
                {
                    using (var response = httpClient.Send(message))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var resultContent = response.Content.ReadAsStringAsync().Result;

                            if (typeof(string) == typeof(TResult))
                            {
                                return new Result<TResult> { Data = resultContent as TResult };
                            }

                            if (string.IsNullOrWhiteSpace(resultContent))
                            {
                                resultContent = "{}";
                            }

                            return new Result<TResult> { Data = JsonSerializer.Deserialize<TResult>(resultContent) };
                        }

                        var errorStr = response.Content.ReadAsStringAsync().Result;
                        HttpErrorData errorData;

                        try
                        {
                            errorData = JsonSerializer.Deserialize<HttpErrorData>(errorStr);
                        }
                        catch
                        {
                            errorData = new HttpErrorData { status = (int)response.StatusCode, error = response.ReasonPhrase, message = errorStr };
                        }


                        return ErrorResult.GlobalError<TResult>(errorData.error ?? "ERROR", errorData.message ?? "Error");
                    }
                }
                catch (Exception ex)
                {
                    return ErrorResult.GlobalError<TResult>(ex.Source ?? "ERROR", ex.Message);
                }
            }
        }

        /// <summary>
        /// Sends http POST request.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TErrorData"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="uri"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static Result<TResult> SendPost<TResult, TErrorData>(this HttpClient httpClient, string uri, IDictionary<string, object>? body)
            where TResult : class
            where TErrorData : class
        {
            using (var message = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                if (null != body)
                {
                    message.Content = JsonContent.Create(body);
                }

                try
                {
                    using (var response = httpClient.Send(message))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            if (typeof(string) == typeof(TResult))
                            {
                                return new Result<TResult> { Data = response.Content.ReadAsStringAsync().Result as TResult };
                            }

                            return new Result<TResult> { Data = response.Content.ReadFromJsonAsync<TResult>().Result };
                        }

                        var errorData = response.Content.ReadAsStringAsync().Result;

                        return ErrorResult.GlobalError<TResult>(errorData);
                    }
                }
                catch (Exception ex)
                {
                    return ErrorResult.GlobalError<TResult>(ex.Source ?? "ERROR", ex.Message);
                }
            }
        }

        /// <summary>
        /// Sends http GET request.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Result<TResult> SendGet<TResult>(this HttpClient httpClient, string uri)
            where TResult : class
        {
            using (var message = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                try
                {
                    using (var response = httpClient.Send(message))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            if (typeof(string) == typeof(TResult))
                            {
                                return new Result<TResult> { Data = response.Content.ReadAsStringAsync().Result as TResult };
                            }

                            return new Result<TResult> { Data = response.Content.ReadFromJsonAsync<TResult>().Result };
                        }

                        var errorData = response.Content.ReadAsStringAsync().Result;

                        return ErrorResult.GlobalError<TResult>(errorData);
                    }
                }
                catch (Exception ex)
                {
                    return ErrorResult.GlobalError<TResult>(ex.Source ?? "ERROR", ex.ToString());
                }
            }
        }

        /// <summary>
        /// Sends http GET request.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<Result<TResult>> SendGetAsync<TResult>(this HttpClient httpClient, string uri)
            where TResult : class
        {
            using (var message = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                try
                {
                    using (var response = await httpClient.SendAsync(message).ConfigureAwait(false))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            if (typeof(string) == typeof(TResult))
                            {
                                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                                return new Result<TResult> { Data = content as TResult };
                            }

                            var data = await response.Content.ReadFromJsonAsync<TResult>().ConfigureAwait(false);
                            return new Result<TResult> { Data = data };
                        }

                        var errorData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return ErrorResult.GlobalError<TResult>(errorData);
                    }
                }
                catch (Exception ex)
                {
                    return ErrorResult.GlobalError<TResult>(ex.Source ?? "ERROR", ex.ToString());
                }
            }
        }

    }
}
