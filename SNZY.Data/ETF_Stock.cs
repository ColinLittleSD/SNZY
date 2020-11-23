using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public int StockId { get; set; }

        [Required]
        public int ETFId { get; set; }
    }
}
