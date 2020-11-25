using SNZY.Data;
using SNZY.Models.ETFPortfolio;
using SNZY.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Services
{
    public class ETFPortfolioService
    {
        public readonly Guid _userId;

        public ETFPortfolioService(Guid userId)
        {
            _userId = userId;
        }

        public async Task<bool> CreateETFPortfolio(ETFPortfolioCreate model)
        {
            var entity = new ETFPortfolio()
            {
                AuthorId = _userId,
                ETFId = model.ETFId,
                PortfolioId = model.PortfolioId
            };

            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.ETFPortfolios.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<IEnumerable<ETFPortfolioListItem>> GetETFPortfolio()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = await ctx.ETFPortfolios
                    .Where(e => e.AuthorId == _userId)
                    .Select(e => new ETFPortfolioListItem
                    {
                        ETFName = e.ETF.Name,
                        Ticker = e.ETF.Ticker
                    }).ToArrayAsync();

                return query;

            }
        }
        public async Task<bool> RemovePortfolioETFs(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var etfInPortfolio =  ctx.ETFPortfolios.Single(etf => etf.ETFId == id && etf.AuthorId == _userId);

                ctx.ETFPortfolios.Remove(etfInPortfolio);

                return await ctx.SaveChangesAsync() == 1;
            }
        }
    }
}
