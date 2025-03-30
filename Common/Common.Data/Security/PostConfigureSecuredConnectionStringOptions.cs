using Common.Data.Options;
using Common.Security;
using Microsoft.Extensions.Options;

namespace Common.Data.Security
{
    /// <summary>
    /// Post configure ConnectionStringOptions.
    /// </summary>
    public class PostConfigureSecuredConnectionStringOptions : IPostConfigureOptions<ConnectionStringOptions>
    {
        /// <summary>
        /// Post configure options.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        public void PostConfigure(string? name, ConnectionStringOptions options)
        {
            options.ConnectionString = SecuredConnections.GetFromRegistry(options.ConnectionString);
        }
    }

}
