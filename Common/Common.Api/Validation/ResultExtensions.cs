using Common.Api.Operations;
using System.Text;

namespace Common.Api.Validation
{
    /// <summary>
	/// Result class extension methods.
	/// </summary>
	public static class ResultExtensions
    {
        /// <summary>
        /// Gets all errors string.
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string CreateErrorText<TItem>(this Result<TItem> result)
            where TItem : class
        {
            if (null == result.Errors)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            for (var j = 0; j < result.Errors!.Length; j++)
            {
                _ = builder.AppendLine($"Error: {result.Errors![j].ErrorMessage}, Property: {result.Errors![j].Property}");
            }

            return builder.ToString();
        }
    }

}
