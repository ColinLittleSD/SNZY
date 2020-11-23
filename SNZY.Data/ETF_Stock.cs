using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Data
{
    public class ETF_Stock
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid AutherId { get; set; }


        [ForeignKey(nameof(Stock))]
        public int StockId { get; set; }
        public virtual Stock Stock { get; set; }


        [ForeignKey(nameof(ETF))]
        public int ETFId { get; set; }
        public virtual ETF ETF { get; set; }
    }
}
