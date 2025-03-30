namespace Common.Security.Registry
{
    /// <summary>
    /// Тип корневого ключа в реестре
    /// </summary>
    public enum RegistryStorageRoot
    {
        Undefined = 0,
        CurrentUser = 1, // HKCU
        LocalMachine = 2 // HKLM
    }

}
