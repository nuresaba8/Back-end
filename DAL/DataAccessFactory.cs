using DAL.EF;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccessFactory
    {
        public static IRepo<stock_market_data, int, stock_market_data> StockMarketData()
        {
            return new StockMarkeRepo();
        }
    }
}
