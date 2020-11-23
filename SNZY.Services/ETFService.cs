using SNZY.Data;
using SNZY.Models;
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
                AutherId = _userId,
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
                    .Where(etf => etf.AutherId == _userId)
                    .Select(etf => new ETFListItem
                    {
                        ETFId = etf.ETFId,
                        Name = etf.Name,
                        Ticker = etf.Ticker,
                        Price = etf.Price,

                        Holdings = ctx.ETF_Stocks.Where(stockInHoldings => stockInHoldings.ETFId == etf.ETFId).Select(stockInHoldings => new ETF_StockListItem
                        {
                            ETFId = etf.ETFId,
                            StockId = stockInHoldings.StockId,
                            StockName = ctx.Stocks.Where(stock => stock.StockId == stockInHoldings.StockId).Select(stock => stock.StockName).FirstOrDefault()

                        }).ToList()

                        //Holdings = etf.Holdings.Select(stockh => new ETF_StockListItem
                        //{
                        //    StockId = stockh.StockId,
                        //    StockName = stockh.StockName
                        //}).ToList()
                    });

                return query.ToArray();

            }
        }
    }
}
