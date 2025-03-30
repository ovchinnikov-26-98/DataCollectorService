namespace Common.WebApi.Logging
{
    /// <summary>
    /// Log message.
    /// </summary>
    public class LogMessage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="parameters"></param>
        public LogMessage(string format, params object?[] parameters)
        {
            Format = format;
            Parameters = parameters;
        }

        /// <summary>
        /// Log message format.
        /// </summary>
        public string Format { get; }

        /// <summary>
        /// Log message parameters.
        /// </summary>
        public object?[] Parameters { get; }
    }

}
