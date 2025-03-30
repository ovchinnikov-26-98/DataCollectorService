using System.ComponentModel.DataAnnotations;

namespace Common.Api.Validation
{
    /// <summary>
    /// Validator.
    /// </summary>
    /// <typeparam name="TContract"></typeparam>
    public interface IValidator<TContract>
        where TContract : class
    {
        /// <summary>
        /// Validator priority.
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Break execution.
        /// </summary>
        bool StopExecution { get; }

        /// <summary>
        /// Validates contract.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="contract"></param>
        ValidationResult Validate(TContract contract);
    }

}
