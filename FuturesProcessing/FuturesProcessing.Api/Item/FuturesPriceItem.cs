namespace FuturesProcessing.Api.Item
{
    public class FuturesPriceItem
    {
        /// <summary>
        /// Время 
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        /// Валютная пара
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
    }
}
