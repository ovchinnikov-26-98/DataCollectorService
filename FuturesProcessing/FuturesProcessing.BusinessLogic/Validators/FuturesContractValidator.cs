using Common.Api.Validation;
using FuturesProcessing.Api.Contract;
using System.Text.RegularExpressions;

namespace FuturesProcessing.BusinessLogic.Validators
{
    public class FuturesContractValidator : IValidatorAsync<FuturesContract>
    {
        public int Priority => ValidatorPriority.Normal;

        public bool StopExecution => false;

        public Task<ValidationResult> ValidateAsync(FuturesContract contract)
        {
            const string pattern = @"^[A-Z0-9-_.]{1,20}$";
            var errors = new List<ValidationFailure>();

            // Предварительно компилируем regex для лучшей производительности
            var regex = new Regex(pattern, RegexOptions.Compiled);

            // Валидация Symbol
            if (string.IsNullOrWhiteSpace(contract.Symbol))
            {
                errors.Add(new ValidationFailure
                {
                    Code = "PARAMNULL",
                    ErrorMessage = $"{nameof(contract.Symbol)} can't be null or whitespace!",
                    Property = nameof(contract.Symbol)
                });
            }
            else if (!regex.IsMatch(contract.Symbol))
            {
                errors.Add(new ValidationFailure
                {
                    Code = "INVALID_FORMAT",
                    ErrorMessage = $"Invalid format for {nameof(contract.Symbol)}. Allowed characters: uppercase letters (A-Z), digits (0-9), hyphen (-), underscore (_), period (.). Max length: 20.",
                    Property = nameof(contract.Symbol)
                });
            }

            return Task.FromResult(new ValidationResult(errors));
        }
    }
}
