using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Models.APIModels
{
    public class ETFAPIModel
    {
        public class Meta
        {
            public string symbol { get; set; }
        }

        public class Value
        {
            public string datetime { get; set; }
            public string open { get; set; }
            public string high { get; set; }
            public string low { get; set; }
            public string close { get; set; }
            public string volume { get; set; }
        }
        public Meta meta { get; set; }
        public List<Value> values { get; set; }
        public string status { get; set; }
    }
}
