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
        public DateTime Datetime { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public string BuyOrHold { get; set; }
    }
}
