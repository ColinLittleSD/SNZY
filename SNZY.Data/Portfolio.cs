using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Data
{
   public class Portfolio
    {
        [Key]
        public int PortfolioID { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<Stock> StocksInPortfolio { get; set; } = new List<Stock>();
        public virtual List<ETF> ETFInPortfolio { get; set; } = new List<ETF>();
    }
}
