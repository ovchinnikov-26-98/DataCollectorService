using Common.Security.Security;
using Microsoft.Win32;

namespace Common.Security.Registry
{
    ///<summary>
    /// Секьюрное хранилище в реестре
    /// </summary>
    public class RegistrySecurityStorage : ISecurityStorage
    {
        /// <summary>
        /// Тип ветки в реестре, где хранятся данные
        /// </summary>
        public RegistryStorageRoot RegistryStorageRoot { get; set; }

        /// <summary>
        /// Имя ключа в реестре, где будут храниться данные
        /// </summary>
        public string BaseKeyName { get; set; } = @"Software\SBRF\SITY";

        /// <summary>
        /// Имя параметра реестра, где хранится секьюрный ключ
        /// </summary>
        public string SecureKeyName { get; set; } = "Key";

        /// <summary>
        /// Имя параметра реестра, где хранится инициализирующий вектор для шифра
        /// </summary>
        public string SecureInitVectorName { get; set; } = "InitVector";

        /// <summary>
        /// Имя ключа в реестре, где хранится зашифрованное значение
        /// </summary>
        public string SecureValueName { get; set; }

        /// <summary>
        /// Вернуть экземпляр враппера над объектом-ключом реестра
        /// </summary>
        /// <returns></returns>
        public virtual IRegistryKey CreateRegistryKey(Microsoft.Win32.RegistryKey key) =>
            new RegistryKey { InnerObject = key };

        /// <summary>
        /// Открыть базовый ключ реестра
        /// </summary>
        /// <returns></returns>
        public virtual IRegistryKey OpenKeyBase()
        {
            IRegistryKey rk;

            switch (RegistryStorageRoot)
            {
                case RegistryStorageRoot.CurrentUser:
                    rk = CreateRegistryKey(Microsoft.Win32.Registry.CurrentUser);
                    break;

                case RegistryStorageRoot.LocalMachine:
                    rk = CreateRegistryKey(Microsoft.Win32.Registry.LocalMachine);
                    break;

                default:
                    throw new ArgumentException("Argument mustbe only CurrentUser or LocalMachine", nameof(RegistryStorageRoot));
            }

            var retVal = rk.OpenSubKey(BaseKeyName)
                            ?? throw new ApplicationException($"Unable open SubKey: {BaseKeyName} for Registry Key: {rk.Name}");

            return retVal;
        }

        /// <summary>
        /// Вернуть зашифрованные данные из реестра
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public (byte[] key, byte[] initVector, byte[] encryptedString) GetSecureValues(string keyName)
        {
            using (var baseKey = OpenKeyBase())
            using (var subKey = baseKey.OpenSubKey(keyName))
            {
                if (subKey == null)
                    throw new ApplicationException(
                        $"Unable open SubKey: {keyName} for Registry Key: {baseKey.Name}");

                return (Convert.FromBase64String(subKey.GetValue<string>(SecureKeyName)),
                    Convert.FromBase64String(subKey.GetValue<string>(SecureInitVectorName)),
                    Convert.FromBase64String(subKey.GetValue<string>(SecureValueName)));
            }
        }
    }

}
