using Common.Security.Security;
using System.Text;

namespace Common.Security.CryptoData
{
    /// <summary>
    /// Класс для получения строки из хранилища
    /// </summary>
    public class StringCryptoData : BaseCryptoData<string>
    {
        /// <summary>
        /// Преобразовать массив байт в строку
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public override string FromBytes(byte[] byteArray) => Encoding.ASCII.GetString(byteArray);
    }

}
