using System.Security.Cryptography;

namespace Common.Security.Security
{
    /// <summary>
    /// Интерфейс создателя алгоритмов
    /// </summary>
    public interface IAlgorithmFactory
    {
        /// <summary>
        /// Алгоритм шифрования
        /// </summary>
        EncryptionAlgorithm EncryptionAlgorithm { get; set; }

        /// <summary>
        /// Создать алгоритм шифрования
        /// </summary>
        /// <returns></returns>
        SymmetricAlgorithm CreateAlgorithm();
    }

}
