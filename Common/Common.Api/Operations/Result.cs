using Common.Api.Validation;

namespace Common.Api.Operations
{
    /// <summary>
    /// Operation result.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class Result<TItem>
        where TItem : class
    {
        /// <summary>
        /// Operation execution result. Null if Errors not null.
        /// </summary>
        public TItem? Data { get; set; }

        /// <summary>
        /// Operation execution warnings. 
        /// </summary>
        public ValidationFailure[]? Warnings { get; set; }


        /// <summary>
        /// Operation execution errors. 
        /// </summary>
        public ValidationFailure[]? Errors { get; set; }
    }
}
