using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Models.ETF
{
    public class ETFCreate
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Ticker { get; set; }

    }
}
