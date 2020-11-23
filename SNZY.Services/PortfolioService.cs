using SNZY.Data;
using SNZY.Models.Portfolio;
using SNZY.Models.StockPortfolio;
using SNZY.WebAPI.Models;
using System;
using System.Collections.Generic;
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

        public bool CreatePortfolio(PortfolioCreate model)
        {
            var entity = new Portfolio()
            {
                AuthorId = _userId,
                Name = model.Name,
            };

            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Portfolios.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PortfolioListItem> GetPortfolio()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Portfolios
                    .Where(port => port.AuthorId == _userId)
                    .Select(port => new PortfolioListItem
                    {
                        
                        Name = port.Name,
                       
                        StocksInPortfolio = port.StocksInPortfolio.Select(stockport => new StockPortfolioListItem
                        {

                            StockName = stockport.Stock.StockName,
                            Ticker = stockport.Stock.Ticker

                        }).ToList()
                    });


                return query.ToArray();
            }
        }
    }
}
