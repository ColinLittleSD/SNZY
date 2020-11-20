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
    public class StockService
    {
        private readonly Guid _userId;

        public StockService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateStock(StockCreate model)
        {
            var entity =
                new Stock()
                {
                    AuthorId = _userId,
                    StockName = model.StockName,
                    Ticker = model.Ticker,
                    Price = model.Price,
                    AverageVolume = model.AverageVolume,
                    MarketCap = model.MarketCap
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Stocks.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<StockListItem> GetStockPosts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Stocks
                        .Where(stock => stock.AuthorId == _userId)
                        .Select(
                            stock =>
                                new StockListItem
                                {
                                    StockName = stock.StockName,
                                    Ticker = stock.Ticker,
                                    Price = stock.Price,
                                    AverageVolume = stock.AverageVolume,
                                    MarketCap = stock.MarketCap
                                }
                        ); ;

                return query.ToArray();
            }
        }
    }
}
