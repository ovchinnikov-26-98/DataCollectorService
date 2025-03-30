using Common.Api.Execution;
using Common.Api.Operations;
using Common.Api.Services;
using DataCollector.Api.Contract;
using DataCollector.Api.Item;
using DataCollector.Api.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector.BusinessLogic.Services
{
    public class FuturesService : ServiceBase, IFuturesService
    {
        public FuturesService(IMediator mediator) : base(mediator)
        {
        }

        public async Task<Result<FuturesPriceItem>> GetFuturesPricesAsync(FuturesContract contract)
        {
            return await ExecuteContractAsync<FuturesContract, FuturesPriceItem>(contract);
        }
    }
}
