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
        [Display(Name = "Stock ID")]
        public int StockId { get; set; }

        [Required]
        [Display(Name ="Stock Name")]
        public string StockName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public string Ticker { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
    }
}
