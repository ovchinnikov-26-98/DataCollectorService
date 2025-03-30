using Microsoft.Extensions.Logging;

namespace Common.Api.Logging
{

    /// <summary>
    /// Default logger delegates.<br/>
    /// <see href="https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1848">CA1848</see>
    /// </summary>
    public static class LogMessages
    {
        /// <summary>
        /// Trace log delegate.
        /// </summary>
        private static readonly Action<ILogger, string, Exception?> TraceLogMessage = LoggerMessage.Define<string>(LogLevel.Trace, new EventId(1), "{ Message }");

        /// <summary>
        /// Debug log delegate.
        /// </summary>
        private static readonly Action<ILogger, string, Exception?> DebugLogMessage = LoggerMessage.Define<string>(LogLevel.Debug, new EventId(200), "{ Message }");

        /// <summary>
        /// Debug log delegate.
        /// </summary>
        private static readonly Action<ILogger, string, Exception?> DebugWithParamsLogMessage = LoggerMessage.Define<string>(LogLevel.Debug, new EventId(200), "{ Message }");

        /// <summary>
        /// Information log delegate.
        /// </summary>
        private static readonly Action<ILogger, string, Exception?> InformationLogMessage = LoggerMessage.Define<string>(LogLevel.Information, new EventId(300), "{ Message }");

        /// <summary>
        /// Warning log delegate.
        /// </summary>
        private static readonly Action<ILogger, string, Exception?> WarningLogMessage = LoggerMessage.Define<string>(LogLevel.Warning, new EventId(400), "{ Message }");

        /// <summary>
        /// Error log delegate.
        /// </summary>
        private static readonly Action<ILogger, string, Exception?> ErrorLogMessage = LoggerMessage.Define<string>(LogLevel.Error, new EventId(500), "{ Message }");

        /// <summary>
        /// Error log delegate.
        /// </summary>
        private static readonly Action<ILogger, string, string, Exception?> ErrorTextLogMessage =
                                            LoggerMessage.Define<string, string>(LogLevel.Error, new EventId(500), $"{{ Message }} {Environment.NewLine} {{ Error }}.");

        /// <summary>
        /// Critical log delegate.
        /// </summary>
        private static readonly Action<ILogger, string, Exception?> CriticalLogMessage = LoggerMessage.Define<string>(LogLevel.Critical, new EventId(700), "{ Message }");

        /// <summary>
        /// Writes a trace log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        public static void Trace(this ILogger logger, string message) => TraceLogMessage(logger, message, default!);

        /// <summary>
        /// Writes a debug log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        public static void Debug(this ILogger logger, string message) => DebugLogMessage(logger, message, default!);

        /// <summary>
        /// Writes an informational log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        public static void Info(this ILogger logger, string message) => InformationLogMessage(logger, message, default!);

        /// <summary>
        /// Writes a warning log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        public static void Warning(this ILogger logger, string message) => WarningLogMessage(logger, message, default!);

        /// <summary>
        /// Writes a warning log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Warning(this ILogger logger, string message, Exception exception) => WarningLogMessage(logger, message, exception);

        /// <summary>
        /// Writes an error log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        public static void Error(this ILogger logger, string message) => ErrorLogMessage(logger, message, default!);

        /// <summary>
        /// Writes an error log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Error(this ILogger logger, string message, Exception exception) => ErrorLogMessage(logger, message, exception);

        /// <summary>
        /// Writes an error log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="errorText"></param>
        public static void Error(this ILogger logger, string message, string errorText) => ErrorTextLogMessage(logger, message, errorText, default!);

        /// <summary>
        /// Writes a critical error log message.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Critical(this ILogger logger, string message, Exception exception) => InformationLogMessage(logger, message, exception);
    }



}
