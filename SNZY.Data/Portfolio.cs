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
        public int PortfolioId { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        [Display(Name = "Portfolio Name")]
        public string Name { get; set; }
        public virtual List<StockPortfolio> StocksInPortfolio { get; set; } = new List<StockPortfolio>();
        public virtual List<ETFPortfolio> ETFInPortfolio { get; set; } = new List<ETFPortfolio>();
    }
}
