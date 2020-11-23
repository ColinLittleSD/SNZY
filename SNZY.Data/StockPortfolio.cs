using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Data
{
    public class StockPortfolio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [ForeignKey(nameof(Stock))]
        public int StockId { get; set; }
        public virtual Stock Stock { get; set; }

        [ForeignKey(nameof(Portfolio))]
        public int PortfolioId { get; set; }
        public virtual Portfolio Portfolio { get; set; }

    }
}
