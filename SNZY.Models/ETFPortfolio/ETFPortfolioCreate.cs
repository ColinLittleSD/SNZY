using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Models.ETFPortfolio
{
    public class ETFPortfolioCreate
    {
        [Required]
        public int ETFId { get; set; }
        [Required]
        public int PortfolioId { get; set; }
    }
}
