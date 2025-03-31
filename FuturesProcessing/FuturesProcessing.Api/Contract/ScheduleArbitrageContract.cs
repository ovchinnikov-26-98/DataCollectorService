using System.ComponentModel.DataAnnotations;

namespace FuturesProcessing.Api.Contract
{
    public class ScheduleArbitrageContract
    {
        [Required]
        public string FirstFutureSymbol { get; init; } = "BTCUSDT_QUARTER";

        [Required]
        public string SecondFutureSymbol { get; init; } = "BTCUSDT_BI-QUARTER";

        [RegularExpression(@"^(\*|([0-9]|\*/[0-9]+)(,([0-9]|\*/[0-9]+))*)( (\*|([0-9]|\*/[0-9]+)(,([0-9]|\*/[0-9]+))*)){5}$",
        ErrorMessage = "Invalid Quartz cron expression (6 fields required)")]
        public string CronExpression { get; init; } = "0/5 * * * * ?";
    }
}
