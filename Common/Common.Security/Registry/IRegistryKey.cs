namespace Common.Security.Registry
{
    /// <summary>
    /// Wrapper for system class ReistryKey - for testing purposes
    /// </summary>
    public interface IRegistryKey : IDisposable
    {
        /// <summary>
        /// Retrieves the name of the key
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Retrieves a subkey as read-only
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IRegistryKey OpenSubKey(string name);

        /// <summary>
        /// Retrieves the value associated with the specified name
        /// Returns null if the name/value pair does not exist in the registry
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        T GetValue<T>(string name) where T : class;
    }

}
