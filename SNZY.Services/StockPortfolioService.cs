using SNZY.Data;
using SNZY.Models.StockPortfolio;
using SNZY.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Services
{
    public class StockPortfolioService
    {
        public readonly Guid _userId;

        public StockPortfolioService(Guid userId)
        {
            _userId = userId;
        }

        public async Task<bool> CreateStockPortfolio(StockPortfolioCreate model)
        {
            var entity = new StockPortfolio()
            {
                AuthorId = _userId,
                StockId = model.StockId,
                PortfolioId = model.PortfolioId
            };

            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.StockPortfolios.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<IEnumerable<StockPortfolioListItem>> GetStockPortfolio()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = await ctx.StockPortfolios
                    .Where(e => e.AuthorId == _userId)
                    .Select(e => new StockPortfolioListItem
                    {
                        StockName = e.Stock.StockName,
                        Ticker = e.Stock.Ticker
                    }).ToArrayAsync();

                return query;

            }
        }

    }
}
