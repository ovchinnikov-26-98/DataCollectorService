using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Security.Security
{
    /// <summary>
    /// Создатель энкриптора/декриптора
    /// </summary>
    public class CryptoTransformFactory : ICryptoTransformFactory
    {
        /// <summary>
        /// Фабрика создателя алгоритмов
        /// </summary>
        public IAlgorithmFactory AlgorithmFactory { get; set; }

        /// <summary>
        /// Вернуть энкриптор
        /// </summary>
        /// <param name="algo"></param>
        /// <param name="key"></param>
        /// <param name="initVector"></param>
        /// <returns></returns>
        public ICryptoTransform CreateEncryptor(byte[] key, byte[] initVector) =>
            AlgorithmFactory.CreateAlgorithm().CreateEncryptor(key, initVector);

        /// <summary>
        /// Вернуть декриптор
        /// </summary>
        /// <param name="algo"></param>
        /// <param name="key"></param>
        /// <param name="initVector"></param>
        /// <returns></returns>
        public ICryptoTransform CreateDecryptor(byte[] key, byte[] initVector) =>
            AlgorithmFactory.CreateAlgorithm().CreateDecryptor(key, initVector);
    }

}
