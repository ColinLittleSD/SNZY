using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Data
{
    public class ETF
    {
        [Key]
        [Display(Name = "ETF ID")]
        public int ETFId { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        [Display(Name = "ETF Name")]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public string Ticker { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual List<ETF_Stock> Holdings { get; set; } = new List<ETF_Stock>();

    }
}
