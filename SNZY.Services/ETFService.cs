using SNZY.Data;
using SNZY.Models;
using SNZY.Models.ETF;
using SNZY.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Services
{
    public class ETFService
    {
        public readonly Guid _userId;

        public ETFService(Guid userId)
        {
            _userId = userId;
        }

        public async Task<bool> CreateETF(ETFCreate model)
        {
            var entity = new ETF()
            {
                AuthorId = _userId,
                Name = model.Name,
                Ticker = model.Ticker,
                Price = model.Price
            };

            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.ETFs.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<IEnumerable<ETFListItem>> GetETFs()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = await ctx.ETFs
                    .Where(etf => etf.AuthorId == _userId)
                    .Select(etf => new ETFListItem
                    {
                        ETFId = etf.ETFId,
                        Name = etf.Name,
                        Ticker = etf.Ticker,
                        Price = etf.Price,

                        Holdings = etf.Holdings.Select(stock => new ETF_StockListItem
                        {
                            StockId = stock.StockId,
                            StockName = stock.Stock.StockName,
                            Ticker = stock.Stock.Ticker

                        }).ToList()
                    }).ToArrayAsync();

                return query;

            }
        }
    }
}
