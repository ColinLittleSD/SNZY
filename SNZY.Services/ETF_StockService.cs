using SNZY.Data;
using SNZY.Models;
using SNZY.WebAPI.Models;
using System;
using System.Collections.Generic;
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
        public bool CreateETF_Stock(ETF_StockCreate model)
        {
            var entity = new ETF_Stock()
            {
                AutherId = _userId,
                StockId = model.StockId,
                ETFId = model.ETFId
            };

            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.ETF_Stocks.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ETF_StockListItem> GetETF_Stocks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.ETF_Stocks
                    .Where(e => e.AutherId == _userId)
                    .Select(e => new ETF_StockListItem
                    {
                        ETFId = e.ETFId,
                        StockId = e.StockId,
                        StockName = ctx.Stocks.Where(stock => stock.StockId == e.StockId).Select(stock => stock.StockName).FirstOrDefault()
                    });

                return query.ToArray();

            }
        }
    }
}
