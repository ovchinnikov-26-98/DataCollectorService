using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Api.Validation
{
    /// <summary>
    /// Validator async.
    /// </summary>
    /// <typeparam name="TContract"></typeparam>
    public interface IValidatorAsync<TContract>
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
        Task<ValidationResult> ValidateAsync(TContract contract);
    }
}
