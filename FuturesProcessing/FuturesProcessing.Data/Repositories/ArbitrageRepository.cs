using Common.Data.ConnectionManager;
using Common.Data.Repositories;
using Dapper;
using FuturesProcessing.Api.Item;
using FuturesProcessing.Api.Repositories;
using Microsoft.Extensions.Logging;

namespace FuturesProcessing.Data.Repositories
{
    public class ArbitrageRepository : RepositoryBase, IArbitrageRepository
    {
        //private readonly ILogger _logger;
        #region SqlQuerys
        const string sqlInsertQuery = @"
                INSERT INTO public.arbitrage_results
(""time"", quarter_price, quarter_symbol, bi_quarter_price, bi_quarter_symbol, difference)
VALUES (@time, @quarter_price, @quarter_symbol, @bi_quarter_price, @bi_quarter_symbol, @difference)";
        #endregion

        public ArbitrageRepository(IConnectionManager connectionManager/*, ILogger logger*/) : base(connectionManager)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            //ArgumentNullException.ThrowIfNull(nameof(logger));

            //_logger = logger;
        }

        public async Task SaveArbitrageResultAsync(ArbitrageResultItem data)
        {
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("difference", data.Difference);
                parameters.Add("bi_quarter_price", data.BiQuarterPrice);
                parameters.Add("quarter_price", data.QuarterPrice);
                parameters.Add("bi_quarter_symbol", data.BiQuarterSymbol);
                parameters.Add("quarter_symbol", data.QuarterSymbol);
                parameters.Add("time", DateTimeOffset.FromUnixTimeMilliseconds(data.Time).DateTime);

                await Connection.ExecuteAsync(sqlInsertQuery, parameters);
            }
            catch (Exception ex)
            {
                 //_logger.LogError(ex, "Error saving Arbitrage data for {difference}", data.Difference);
                throw;
            }
        }
    }
}
