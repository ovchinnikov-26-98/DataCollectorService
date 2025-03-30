using System.Security.Cryptography;

namespace Common.Security.Security
{
    /// <summary>
    /// Создатель алгоритмов
    /// </summary>
    public class AlgorithmFactory : IAlgorithmFactory
    {
        /// <summary>
        /// Алгоритм шифрования
        /// </summary>
        public EncryptionAlgorithm EncryptionAlgorithm { get; set; }

        /// <summary>
        /// Создать алгоритм шифрования и проинициализировать его ключем и исходными данными
        /// </summary>
        /// <returns></returns>
        public virtual SymmetricAlgorithm CreateAlgorithm()
        {
            var retVal = CreateConcreteAlgorithm(EncryptionAlgorithm);
            retVal.Mode = CipherMode.CBC;

            return retVal;
        }

        /// <summary>
        /// Создать алгоритм шифрования
        /// </summary>
        /// <param name="algo"></param>
        /// <returns></returns>
        protected virtual SymmetricAlgorithm CreateConcreteAlgorithm(EncryptionAlgorithm algo)
        {
            switch (algo)
            {
                case EncryptionAlgorithm.Des: return new DESCryptoServiceProvider();

                case EncryptionAlgorithm.Rc2: return new RC2CryptoServiceProvider();

                case EncryptionAlgorithm.Rijndael: return new RijndaelManaged();

                case EncryptionAlgorithm.TripleDes: return new TripleDESCryptoServiceProvider();

                default:
                    throw new ArgumentException("Unsupported type of algorithm!", nameof(algo));
            }
        }

    }
}
