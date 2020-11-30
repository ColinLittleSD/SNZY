using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Models
{
    public class StockDetail
    {
        public int StockId { get; set; }
        public string StockName { get; set; }
        public string Ticker { get; set; }
        public double Price { get; set; }
        public double AverageVolume { get; set; }
        public double MarketCap { get; set; }
    }
}
