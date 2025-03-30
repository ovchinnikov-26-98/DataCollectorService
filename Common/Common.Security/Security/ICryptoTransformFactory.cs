using System.Security.Cryptography;

namespace Common.Security.Security
{
    /// <summary>
    /// Интерфейс создателя энкриптора/декриптора
    /// </summary>
    public interface ICryptoTransformFactory
    {
        /// <summary>
        /// Фабрика создателя алгоритмов
        /// </summary>
        IAlgorithmFactory AlgorithmFactory { get; set; }

        /// <summary>
        /// Вернуть энкриптор
        /// </summary>
        /// <param name="algo"></param>
        /// <param name="key"></param>
        /// <param name="initVector"></param>
        /// <returns></returns>
        ICryptoTransform CreateDecryptor(byte[] key, byte[] initVector);

        /// <summary>
        /// Вернуть декриптор
        /// </summary>
        /// <param name="algo"></param>
        /// <param name="key"></param>
        /// <param name="initVector"></param>
        /// <returns></returns>
        ICryptoTransform CreateEncryptor(byte[] key, byte[] initVector);
    }

}
