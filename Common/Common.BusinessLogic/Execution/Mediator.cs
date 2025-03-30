using Common.Api.Execution;
using Common.Api.Operations;
using Common.Api.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Common.BusinessLogic.Execution
{
    /// <summary>
    /// Implementation of <see cref="IMediator"/>
    /// </summary>
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="executionContextProvider"></param>
        /// <param name="serviceProvider"></param>
        public Mediator(
            IServiceProvider serviceProvider
        )
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);

            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public Result<TResultValue> Execute<TContract, TResultValue>(TContract contract)
            where TContract : class
            where TResultValue : class
        {
            try
            {

                var validators = _serviceProvider.GetServices<IValidator<TContract>>();

                var validationErrors = new List<ValidationFailure>();

                foreach (var validator in validators)
                {
                    var validationResult = validator.Validate(contract);

                    if (!validationResult.IsValid)
                    {
                        validationErrors.AddRange(validationResult.Errors);

                        if (validator.StopExecution)
                        {
                            break;
                        }
                    }
                }

                if (0 < validationErrors.Count)
                {
                    return new Result<TResultValue> { Errors = [.. validationErrors] };
                }

                var operation = _serviceProvider.GetService<IOperation<TContract, TResultValue>>();

                var result = operation!.Execute(contract);

                return result;
            }
            catch (Exception ex)
            {
                var result = new Result<TResultValue> { Errors = [new ValidationFailure { ErrorMessage = ex.ToString() }] };

                return result;
            }
        }

        public async Task<Result<TResultValue>> ExecuteAsync<TContract, TResultValue>(TContract contract)
    where TContract : class
    where TResultValue : class
        {
            try
            {
                // 1. Валидация
                var validators = _serviceProvider.GetServices<IValidatorAsync<TContract>>();
                var validationErrors = new List<ValidationFailure>();

                foreach (var validator in validators)
                {
                    var validationResult = await validator.ValidateAsync(contract).ConfigureAwait(false);

                    if (!validationResult.IsValid)
                    {
                        validationErrors.AddRange(validationResult.Errors);

                        if (validator.StopExecution)
                        {
                            break;
                        }
                    }
                }

                if (validationErrors.Count > 0)
                {
                    return new Result<TResultValue> { Errors = validationErrors.ToArray() };
                }

                // 2. Выполнение операции
                var operation = _serviceProvider.GetRequiredService<IOperationAsync<TContract, TResultValue>>();

                if (operation is IOperationAsync<TContract, TResultValue> asyncOperation)
                {
                    return await asyncOperation.ExecuteAsync(contract).ConfigureAwait(false);
                }

                return await operation.ExecuteAsync(contract);
            }
            catch (Exception ex)
            {
                return new Result<TResultValue>
                {
                    Errors = new[]
                    {
                        new ValidationFailure
                        {
                            ErrorMessage = ex.Message
                        }
                    }
                };
            }
        }

    }
}
