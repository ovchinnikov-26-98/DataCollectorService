using Common.Api.Operations;

namespace Common.Api.Execution
{
    /// <summary>
    /// Service bus.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Exetes given contract againdt bus.
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <typeparam name="TResultvale"></typeparam>
        /// <param name="contract"></param>
        /// <returns></returns>
        Result<TResultValue> Execute<TContract, TResultValue>(TContract contract)
            where TContract : class
            where TResultValue : class;

        /// <summary>
        /// Exetes given contract againdt bus.
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <typeparam name="TResultvale"></typeparam>
        /// <param name="contract"></param>
        /// <returns></returns>
        Task<Result<TResultValue>> ExecuteAsync<TContract, TResultValue>(TContract contract)
            where TContract : class
            where TResultValue : class;
    }
}
