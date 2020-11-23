using SNZY.Data;
using SNZY.Models.ETFPortfolio;
using SNZY.WebAPI.Models;
using System;
using System.Collections.Generic;
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

        public bool CreateETFPortfolio(ETFPortfolioCreate model)
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
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ETFPortfolioListItem> GetETFPortfolio()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.ETFPortfolios
                    .Where(e => e.AuthorId == _userId)
                    .Select(e => new ETFPortfolioListItem
                    {
                        ETFName = e.ETF.Name,
                        Ticker = e.ETF.Ticker
                    });

                return query.ToArray();

            }
        }
    }
}
