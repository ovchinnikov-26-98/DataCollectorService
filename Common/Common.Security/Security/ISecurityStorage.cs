namespace Common.Security.Security
{

    /// <summary>
    /// Базовый интерфейс секьюрного хранилища
    /// </summary>
    public interface ISecurityStorage
    {
        /// <summary>
        /// Вернуть зашифрованные данные из хранилища
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        (byte[] key, byte[] initVector, byte[] encryptedString) GetSecureValues(string key);
    }

}
