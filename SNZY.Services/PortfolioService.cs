using SNZY.Data;
using SNZY.Models.ETFPortfolio;
using SNZY.Models.Portfolio;
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
    public class PortfolioService
    {
        public readonly Guid _userId;

        public PortfolioService(Guid userId)
        {
            _userId = userId;
        }

        public async Task<bool> CreatePortfolio(PortfolioCreate model)
        {
            var entity = new Portfolio()
            {
                AuthorId = _userId,
                Name = model.Name,
            };

            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Portfolios.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<IEnumerable<PortfolioListItem>> GetPortfolio()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var service = new ShareAPIService();

                var query = await ctx.Portfolios
                    .Where(port => port.AuthorId == _userId)
                    .Select(port => new PortfolioListItem
                    {
                        
                        Name = port.Name,
                       
                        StocksInPortfolio = port.StocksInPortfolio.Select(stockport => new StockPortfolioListItem
                        {
                            StockId = stockport.Stock.StockId,
                            StockName = stockport.Stock.StockName,
                            Ticker = stockport.Stock.Ticker

                        }).ToList(),

                        ETFsInPortfolio = port.ETFInPortfolio.Select(etfPort => new ETFPortfolioListItem
                        {
                            ETFId = etfPort.ETF.ETFId,
                            ETFName = etfPort.ETF.Name,
                            Ticker = etfPort.ETF.Ticker
                        }).ToList()

                    }).ToArrayAsync();

                return query;
            }
        }
    }
}
