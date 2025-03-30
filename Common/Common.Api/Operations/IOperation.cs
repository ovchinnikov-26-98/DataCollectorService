namespace Common.Api.Operations
{
    /// <summary>
    /// Operation.
    /// </summary>
    /// <typeparam name="TContract">Operation contract.</typeparam>
    /// <typeparam name="TResultValue">Operation result value.</typeparam>
    public interface IOperation<TContract, TResultValue>
        where TContract : class
        where TResultValue : class
    {
        /// <summary>
        /// Executes operation with given contract using given execution context.
        /// </summary>
        /// <param name="context">Operation execution context.</param>
        /// <param name="contract">Input for operation.</param>
        /// <returns>Operation execution result wrapped in Maybe-like <see cref="Result{TValue}"/> object.</returns>
        Result<TResultValue> Execute(TContract contract);
    }

}
