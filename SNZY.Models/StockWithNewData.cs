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
        public double Price { get; set; }
<<<<<<< HEAD
        public double MarketCap { get; set; }
=======
        //public double AverageVolume { get; set; }
        public double MarketCap { get; set; }

>>>>>>> 3245d8c1ceb46b79881545a5922bf67530fc67c6
        public DateTime Datetime { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
<<<<<<< HEAD
=======

>>>>>>> 3245d8c1ceb46b79881545a5922bf67530fc67c6
        public string BuyOrHold { get; set; }
    }
}
