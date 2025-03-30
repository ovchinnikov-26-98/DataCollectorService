namespace Common.Api.Validation
{
    /// <summary>
    /// Validation failure
    /// </summary>
    public class ValidationFailure
    {
        /// <summary>
        /// Error code
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Property name
        /// </summary>
        public string? Property { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string? ErrorMessage { get; set; }
    }
}
