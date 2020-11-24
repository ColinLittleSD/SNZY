using SNZY.Data;
using SNZY.Models;
using SNZY.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Services
{
    public class ETF_StockService
    {
        public readonly Guid _userId;

        public ETF_StockService(Guid userId)
        {
            _userId = userId;
        }
        public async Task<bool> CreateETF_Stock(ETF_StockCreate model)
        {
            var entity = new ETF_Stock()
            {
                AuthorId = _userId,
                StockId = model.StockId,
                ETFId = model.ETFId
            };

            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.ETF_Stocks.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<IEnumerable<ETF_StockListItem>> GetETF_Stocks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = await ctx.ETF_Stocks
                    .Where(e => e.AuthorId == _userId)
                    .Select(e => new ETF_StockListItem
                    {
                        StockId = e.StockId,
                        StockName = e.Stock.StockName,
                        Ticker = e.Stock.Ticker
                    }).ToArrayAsync();

                return query;

            }
        }
    }
}
