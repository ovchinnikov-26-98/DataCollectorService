using System.Security.Cryptography;

namespace Common.Security.Security
{
    /// <summary>
    /// Обертка над типом MemoryStream для тестирования
    /// </summary>
    public interface IMemoryStream : IDisposable
    {
        byte[] ToArray();
    }

    /// <summary>
    /// Обертка над типом CryptoStream для тестирования
    /// </summary>
    public interface ICryptoStream : IDisposable
    {
        void Write(byte[] buffer, int offset, int count);
        void FlushFinalBlock();
        void Close();
    }

    /// <summary>
    /// Реализация обертки над типом MemoryStream для тестирования
    /// </summary>
    public class MemStream : MemoryStream, IMemoryStream { }

    /// <summary>
    /// Реализация обертки над типом CryptoStream для тестирования
    /// </summary>
    public class CrypStream : CryptoStream, ICryptoStream
    {
        public CrypStream(
            IMemoryStream stream,
            ICryptoTransform transform,
            CryptoStreamMode mode) : base(stream as Stream, transform, mode) { }
    }

    /// <summary>
    /// "Расшифровыватель" данных
    /// </summary>
    public class DecryptData : IDataDecryptor
    {
        /// <summary>
        /// Ссылка на расшифровыватель
        /// </summary>
        public ICryptoTransform CryptoTransform { get; set; }

        /// <summary>
        /// Создать тип - обертку над MemoryStream (вынесено для упрощения тестирования)
        /// </summary>
        /// <returns></returns>
        public virtual IMemoryStream CreateDecryptedStream() => new MemStream();

        /// <summary>
        /// Создать тип - обертку над CryptoStream (вынесено для упрощения тестирования)
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public virtual ICryptoStream CreateCryptoStream(
            IMemoryStream stream,
            CryptoStreamMode mode) => new CrypStream(stream, CryptoTransform, mode);

        /// <summary>
        /// Расшифровать входящие данные
        /// </summary>
        /// <param name="bytesData"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] bytesData)
        {
            using (var decryptedStream = CreateDecryptedStream())
            {
                using (var cryptoStream =
                    CreateCryptoStream(decryptedStream, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytesData, 0, bytesData.Length);
                    cryptoStream.FlushFinalBlock();
                    cryptoStream.Close();
                }

                return decryptedStream.ToArray();
            }
        }
    }

}
