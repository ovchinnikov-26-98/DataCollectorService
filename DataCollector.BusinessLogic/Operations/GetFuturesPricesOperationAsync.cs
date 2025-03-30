using Common.Api.Operations;
using Common.BusinessLogic.Http;
using DataCollector.Api.Contract;
using DataCollector.Api.Item;
using DataCollector.Api.Repositories;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;

namespace DataCollector.BusinessLogic.Operations
{
    public class GetFuturesPricesOperationAsync : IOperationAsync<FuturesContract, FuturesPriceItem>
    {
        private readonly HttpClient _httpClient;
        private readonly IFuturesRepository _futuresRepository;

        public GetFuturesPricesOperationAsync(HttpClient httpClient, IFuturesRepository futuresRepository)
        {
            ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
            ArgumentNullException.ThrowIfNull(futuresRepository, nameof(futuresRepository));

            _httpClient = httpClient;
            _futuresRepository = futuresRepository;
        }
        public async Task<Result<FuturesPriceItem>> ExecuteAsync(FuturesContract contract)
        {
            string url = $"fapi/v1/ticker/price?symbol={contract.Symbol}";

            var response = await _httpClient.SendGetAsync<FuturesPriceItem>(url);
            if (response.Errors != null)
            {
                return ErrorResult.GlobalError<FuturesPriceItem>("Error receiving data");
            }
            var result = response.Data;

            if (result != null)
            {
                await _futuresRepository.AddAsync(response.Data);
            }
            else
            {
                result =  await _futuresRepository.GetLatestAsync(contract.Symbol);
            }

            return new Result<FuturesPriceItem>()
            {
                Data = response.Data
            };
        }
    }
}
