namespace Common.Security.Security
{
    /// <summary>
    /// Интерфейс хранилища зашифрованных строк соединения
    /// </summary>
    public interface ICryptoData<T>
    {
        /// <summary>
        /// Создатель энкриптора/декриптора
        /// </summary>
        ICryptoTransformFactory CryptoTransformFactory { get; set; }

        /// <summary>
        /// Создать Энкриптор
        /// </summary>
        /// <param name="key"></param>
        /// <param name="initVector"></param>
        /// <returns></returns>
        IDataDecryptor CreateDecryptor(byte[] key, byte[] initVector);

        /// <summary>
        /// Вернуть строку соединения
        /// </summary>
        /// <param name="encryptedString"></param>
        /// <returns></returns>
        byte[] GetValue((byte[] key, byte[] initVector, byte[] encryptedString) cryptoValues);

        /// <summary>
        /// Получить значение объекта из хранилища по "имени" объекта
        /// </summary>
        /// <param name="objectKey"></param>
        /// <returns></returns>
        T GetValue(string objectKey);
    }

}
