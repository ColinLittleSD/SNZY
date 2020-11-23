using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Models
{
    public class ETF_StockCreate
    {
        [Required]
        public int StockId { get; set; }

        [Required]
        public int ETFId { get; set; }
    }
}
