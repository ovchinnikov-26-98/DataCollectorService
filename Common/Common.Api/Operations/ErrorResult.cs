using Common.Api.Validation;

namespace Common.Api.Operations
{
    /// <summary>
    /// Error result builder.
    /// </summary>
    public static class ErrorResult
    {
        /// <summary>
        /// Creates global error result.
        /// </summary>
        /// <typeparam name="TResultValue"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Result<TResultValue> GlobalError<TResultValue>(string message)
            where TResultValue : class
        {
            return GlobalError<TResultValue>("ERROR", message);
        }

        /// <summary>
        /// Creates global error result.
        /// </summary>
        /// <typeparam name="TResultValue"></typeparam>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Result<TResultValue> GlobalError<TResultValue>(string code, string message)
            where TResultValue : class
        {
            var error = new ValidationFailure { Code = code, ErrorMessage = message };

            return new Result<TResultValue> { Errors = new[] { error } };
        }

        /// <summary>
        /// Creates global error result.
        /// </summary>
        /// <typeparam name="TResultValue"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Result<TResultValue> GlobalErrorFromResult<TResultValue>(Result<TResultValue> result)
            where TResultValue : class
        {
            return new Result<TResultValue> { Errors = result.Errors };
        }
    }

}
