namespace FuturesProcessing.Api.Item
{
    public class PriceProcessedItem
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Время открытия Kline
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Валютная пара
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Цена открытия
        /// </summary>
        public decimal OpenPrice { get; set; }

        /// <summary>
        /// Высшая цена
        /// </summary>
        public decimal HighPrice { get; set; }

        /// <summary>
        /// Низшая цена
        /// </summary>
        public decimal LowPrice { get; set; }

        /// <summary>
        /// Цена закрытия
        /// </summary>
        public decimal ClosePrice { get; set; }
    }
}
