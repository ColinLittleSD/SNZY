using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Data
{
    public class Stock
    {
        [Key]
        public int StockId { get; set; }

        [Required]
        public string StockName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public string Ticker { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double AverageVolume { get; set; }

        [Required]
        public double MarketCap { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
    }
}
