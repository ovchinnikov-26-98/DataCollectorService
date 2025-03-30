using Common.Data.ConnectionManager;
using Common.Data.Repositories;
using Dapper;
using DataCollector.Api.Item;
using DataCollector.Api.Repositories;
using Microsoft.Extensions.Logging;

namespace DataCollector.Data.Repositories
{
    public class FuturesRepository : RepositoryBase, IFuturesRepository
    {

        #region SqlQuerys
        const string sqlInsertQuery = @"
                INSERT INTO public.futures_data
(symbol, price, currenttimestamp)
                VALUES (@Symbol, @Price, @Timestamp)";



        const string sqlSelectQuery = @"
                SELECT * FROM futures_data 
                WHERE symbol = @Symbol 
                ORDER BY timestamp DESC 
                LIMIT 1";
        #endregion


        //private readonly ILogger _logger;

        public FuturesRepository(IConnectionManager connectionManager/*, ILogger logger*/) : base(connectionManager)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            //ArgumentNullException.ThrowIfNull(nameof(logger));

            //_logger = logger;
        }

        public async Task<FuturesPriceItem> GetLatestAsync(string symbol)
        {
            try
            {
                return await Connection.QueryFirstOrDefaultAsync<FuturesPriceItem>(sqlSelectQuery, new { Symbol = symbol });
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error getting latest futures data for {Symbol}", symbol);
                throw;
            }
        }

        public async Task AddAsync(FuturesPriceItem data)
        {
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("Symbol", data.Symbol);
                parameters.Add("Price", data.Price );
                parameters.Add("Timestamp", DateTimeOffset.FromUnixTimeMilliseconds(data.Time).DateTime);

                await Connection.ExecuteAsync(sqlInsertQuery, parameters);
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex, "Error saving futures data for {Symbol}", data.Symbol);
                throw;
            }
        }
    }
}
