using System.ComponentModel.DataAnnotations;

namespace FuturesProcessing.Api.Contract
{
    public class FuturesCoupleContract
    {

        [Required]
        public string FirstFutureSymbol { get; init; } = "BTCUSDT_QUARTER";

        [Required]
        public string SecondFutureSymbol { get; init; } = "BTCUSDT_BI-QUARTER";

    }
}
