using Common.Api.Operations;
using FuturesProcessing.Api.Contract;
using FuturesProcessing.Api.DataProviders;
using FuturesProcessing.Api.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuturesProcessing.BusinessLogic.Operations
{
    public class GetDataOperation : IOperationAsync<FuturesContract, FuturesPriceItem>
    {
        private readonly IDataProvider _provider;

        public GetDataOperation(IDataProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));

            _provider = provider;
        }

        public async Task<Result<FuturesPriceItem>> ExecuteAsync(FuturesContract contract)
        {
            var result = await _provider.FetchDataAsync(contract);

            return result;
        }
    }
}
