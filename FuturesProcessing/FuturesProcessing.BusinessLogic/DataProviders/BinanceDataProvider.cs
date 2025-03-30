using Common.Api.Operations;
using Common.BusinessLogic.Http;
using FuturesProcessing.Api.Contract;
using FuturesProcessing.Api.DataProviders;
using FuturesProcessing.Api.Item;
using System.Diagnostics.Contracts;

namespace FuturesProcessing.BusinessLogic.DataProviders
{
    public class BinanceDataProvider : IDataProvider
    {
        private readonly HttpClient _httpClient;

        public BinanceDataProvider(HttpClient httpClient)
        {
            ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));

            _httpClient = httpClient;
        }

        public async Task<Result<FuturesPriceItem>> FetchDataAsync(FuturesContract contract)
        {
            var url = $"api/v1/Futures/GetPrices?Symbol={contract.Symbol}";

            var response = await _httpClient.SendGetAsync<Result<FuturesPriceItem>>(url);
            if(response.Errors != null)
            {
                ErrorResult.GlobalError<FuturesPriceItem>("Error receiving data");
            }
            return response.Data;
        }
    }
}
