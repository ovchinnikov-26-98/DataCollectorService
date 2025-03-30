using System.Security.Cryptography;

namespace Common.Security.Security
{
    /// <summary>
    /// Интерфейс "Расшифровывателя" данных
    /// </summary>
    public interface IDataDecryptor
    {
        /// <summary>
        /// Ссылка на расшифровыватель
        /// </summary>
        ICryptoTransform CryptoTransform { get; set; }

        /// Расшифровать входящие данные
        byte[] Decrypt(byte[] bytesData);
    }

}
