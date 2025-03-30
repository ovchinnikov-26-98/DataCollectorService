using System.Text;

namespace Common.Api.Validation
{
    /// <summary>
    /// Validation result.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Success result.
        /// </summary>
        public static ValidationResult Success { get; }

        static ValidationResult()
        {
            Success = new ValidationResult();
        }

        /// <summary>
        /// Whether validation succeeded.
        /// </summary>
        public bool IsValid => 0 == Errors.Count;

        /// <summary>
        /// A collection of errors.
        /// </summary>
        public IReadOnlyCollection<ValidationFailure> Errors { get; }

        /// <summary>
        /// Creates a new validationResult.
        /// </summary>
        public ValidationResult()
        {
            Errors = Array.Empty<ValidationFailure>();
        }

        /// <summary>
        /// Creates a new ValidationResult from a collection of failures.
        /// </summary>
        public ValidationResult(IReadOnlyCollection<ValidationFailure> failures)
        {
            Errors = failures;
        }

        /// <summary>
        /// Generates a string representation of the error messages separated by new lines.
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var error in Errors)
            {
                _ = sb.AppendLine(error.ErrorMessage);
            }

            return sb.ToString();
        }
    }

}
