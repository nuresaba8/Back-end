using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class StockMarketDTO
    {
        public short id { get; set; }
        public string trade_code { get; set; }
        public string date {  get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float open { get; set; }
        public float close { get; set; }
        public float volume { get; set; }
    }
}
