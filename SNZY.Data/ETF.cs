﻿using System;
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
        public int ETFId { get; set; }

        [Required]
        public Guid AutherId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Ticker { get; set; }

        [Required]
        public decimal Price { get; set; }

        //[Required]
        //public List<Stock> Holdings { get; set; }

    }
}
