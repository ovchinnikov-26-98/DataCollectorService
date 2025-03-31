namespace FuturesProcessing.Api.Item
{
    public class ArbitrageResultItem
    {
        public long Time { get; init; }
        public decimal QuarterPrice { get; init; }
        public decimal BiQuarterPrice { get; init; }
        public string? QuarterSymbol { get; init; }
        public string? BiQuarterSymbol { get; init; }
        public decimal Difference { get; init; }
    }
}
