using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Models.StockPortfolio
{
    public class StockPortfolioCreate
    {
        [Required]
        public int StockId { get; set; }
        [Required]
        public int PortfolioId { get; set; }
    }
}
