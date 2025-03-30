namespace Common.Security.Registry
{
    // <summary>
    /// Implementation of a wrapper for system class ReistryKey - for testing purposes
    /// </summary>
    public class RegistryKey : IRegistryKey
    {
        public string? Name
        {
            get => InnerObject?.Name;
        }

        public Microsoft.Win32.RegistryKey InnerObject { get; set; }

        public T GetValue<T>(string name) where T : class => (T)InnerObject.GetValue(name);

        public IRegistryKey OpenSubKey(string name) =>
            new RegistryKey { InnerObject = InnerObject.OpenSubKey(name) };

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    InnerObject?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RegistryKey() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }

}
