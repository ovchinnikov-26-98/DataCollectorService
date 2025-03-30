using Common.Api.Execution;
using Common.Api.Operations;

namespace Common.Api.Services
{
    /// <summary>
    /// Base class for Services.
    /// </summary>
    public class ServiceBase
    {
        private readonly IMediator _mediator;

        protected ServiceBase(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(nameof(mediator));

            _mediator = mediator;
        }

        protected Result<TResultValue> ExecuteContract<TContract, TResultValue>(TContract contract)
            where TContract : class
            where TResultValue : class
        {
            return _mediator.Execute<TContract, TResultValue>(contract);
        }

        protected async Task<Result<TResultValue>> ExecuteContractAsync<TContract, TResultValue>(TContract contract)
            where TContract : class
            where TResultValue : class
        {
            return await _mediator.ExecuteAsync<TContract, TResultValue>(contract);
        }
    }
}
