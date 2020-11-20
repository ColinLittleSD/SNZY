using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Models
{
    public class ETF_StockListItem
    {
        public int StockId { get; set; }
        public string StockName { get; set; }

        public int ETFId { get; set; }

        //public string ETFName { get; set; }
    }
}
