namespace Common.BusinessLogic.Http
{
    /// <summary>
    /// Http error data.
    /// </summary>
    /// <param name="timestamp"></param>
    /// <param name="status"></param>
    /// <param name="error"></param>
    /// <param name="message"></param>
    public record struct HttpErrorData(long timestamp, int status, string? error, string? message);

}
