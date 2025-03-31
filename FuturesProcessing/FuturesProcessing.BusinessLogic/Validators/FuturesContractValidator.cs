using Common.Api.Validation;
using FuturesProcessing.Api.Contract;
using System.Text.RegularExpressions;

namespace FuturesProcessing.BusinessLogic.Validators
{
    public class FuturesContractValidator : IValidatorAsync<FuturesCoupleContract>
    {
        public int Priority => ValidatorPriority.Normal;

        public bool StopExecution => false;

        public Task<ValidationResult> ValidateAsync(FuturesCoupleContract contract)
        {
            const string pattern = @"^[A-Z0-9-_.]{1,20}$";
            var errors = new List<ValidationFailure>();

            // Предварительно компилируем regex для лучшей производительности
            var regex = new Regex(pattern, RegexOptions.Compiled);

            
            if (string.IsNullOrWhiteSpace(contract.FirstFutureSymbol))
            {
                errors.Add(new ValidationFailure
                {
                    Code = "PARAMNULL",
                    ErrorMessage = $"{nameof(contract.FirstFutureSymbol)} can't be null or whitespace!",
                    Property = nameof(contract.FirstFutureSymbol)
                });
            }
            else if (!regex.IsMatch(contract.FirstFutureSymbol))
            {
                errors.Add(new ValidationFailure
                {
                    Code = "INVALID_FORMAT",
                    ErrorMessage = $"Invalid format for {nameof(contract.FirstFutureSymbol)}. Allowed characters: uppercase letters (A-Z), digits (0-9), hyphen (-), underscore (_), period (.). Max length: 20.",
                    Property = nameof(contract.FirstFutureSymbol)
                });
            }

            if (string.IsNullOrWhiteSpace(contract.SecondFutureSymbol))
            {
                errors.Add(new ValidationFailure
                {
                    Code = "PARAMNULL",
                    ErrorMessage = $"{nameof(contract.SecondFutureSymbol)} can't be null or whitespace!",
                    Property = nameof(contract.SecondFutureSymbol)
                });
            }
            else if (!regex.IsMatch(contract.SecondFutureSymbol))
            {
                errors.Add(new ValidationFailure
                {
                    Code = "INVALID_FORMAT",
                    ErrorMessage = $"Invalid format for {nameof(contract.SecondFutureSymbol)}. Allowed characters: uppercase letters (A-Z), digits (0-9), hyphen (-), underscore (_), period (.). Max length: 20.",
                    Property = nameof(contract.SecondFutureSymbol)
                });
            }

            return Task.FromResult(new ValidationResult(errors));
        }
    }
}
