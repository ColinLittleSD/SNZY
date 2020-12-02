using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Models
{
    public class StockCreate
    {
        [Required]
        public string StockName { get; set; }

        [Required]
        [MaxLength(5)]
        [MinLength(1)]
        public string Ticker { get; set; }

    }
}
