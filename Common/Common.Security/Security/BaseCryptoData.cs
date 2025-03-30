namespace Common.Security.Security
{
    /// <summary>

    /// Хранилище зашифрованных строк соединения

    /// </summary>

    public abstract class BaseCryptoData<T> : ICryptoData<T>

    {

        /// <summary>

        /// Создатель энкриптора/декриптора

        /// </summary>

        public ICryptoTransformFactory CryptoTransformFactory { get; set; }



        /// <summary>

        /// Хранилище данных

        /// </summary>

        public ISecurityStorage Storage { get; set; }



        /// <summary>

        /// Создать Энкриптор

        /// </summary>

        /// <param name="key"></param>

        /// <param name="initVector"></param>

        /// <returns></returns>

        public virtual IDataDecryptor CreateDecryptor(byte[] key, byte[] initVector) =>

              new DecryptData { CryptoTransform = CryptoTransformFactory.CreateDecryptor(key, initVector) };



        /// <summary>

        /// Получить расшифрованные данные из хранилища в виде массива байт

        /// </summary>

        /// <param name="encryptedString"></param>

        /// <returns></returns>

        public virtual byte[] GetValue((byte[] key, byte[] initVector, byte[] encryptedString) initVals) =>

              CreateDecryptor(initVals.key, initVals.initVector).Decrypt(initVals.encryptedString);



        /// <summary>

        /// Получить расшифрованные данные из хранилища в виде объекта

        /// </summary>

        /// <param name="objectKey"></param>

        /// <returns></returns>

        public virtual T GetValue(string objectKey) => FromBytes(GetValue(Storage.GetSecureValues(objectKey)));



        /// <summary>

        /// Преобразовать массив байт в объект

        /// </summary>

        /// <param name="byteArray"></param>

        /// <returns></returns>

        public abstract T FromBytes(byte[] byteArray);

    }
}
