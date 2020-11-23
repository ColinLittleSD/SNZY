using SNZY.Data;
using SNZY.Models.ETF;
using SNZY.WebAPI.Models;
using System;
using System.Collections.Generic;
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

        public bool CreateETF(ETFCreate model)
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
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ETFListItem> GetETFs()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.ETFs
                    .Where(etf => etf.AuthorId == _userId)
                    .Select(etf => new ETFListItem
                    {
                        Name = etf.Name,
                        Ticker = etf.Ticker,
                        Price = etf.Price
                    });

                return query.ToArray();

            }
        }
    }
}
