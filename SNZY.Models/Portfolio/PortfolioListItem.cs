using SNZY.Models.ETFPortfolio;
using SNZY.Models.StockPortfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Models.Portfolio
{
    public class PortfolioListItem
    {
        public string Name { get; set; }

        public List<StockPortfolioListItem> StocksInPortfolio = new List<StockPortfolioListItem>();

        public List<ETFPortfolioListItem> ETFsInPortfolio = new List<ETFPortfolioListItem>();
    }
}
