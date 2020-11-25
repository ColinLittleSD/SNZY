using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Models
{
    public class StockWithNewData
    {
        public string StockName { get; set; }
        public string Ticker { get; set; }
        public string Price { get; set; }
        public double AverageVolume { get; set; }
        public double MarketCap { get; set; }

        public string Datetime { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Close { get; set; }
        public string Volume { get; set; }

        public string BuyOrHold { get; set; }
    }
}
