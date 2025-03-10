using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class StockMarketService
    {
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<stock_market_data, StockMarketDTO>();
                cfg.CreateMap<StockMarketDTO, stock_market_data>();
            });
            return new Mapper(config);
        }
        public static List<StockMarketDTO> Get(int page, int pageSize)
        {
            var repo = DataAccessFactory.StockMarketData();
            return GetMapper().Map<List<StockMarketDTO>>(repo.Get(page, pageSize));
        }

        public static List<StockMarketDTO> Get()
        {
            var repo = DataAccessFactory.StockMarketData();
            return GetMapper().Map<List<StockMarketDTO>>(repo.Get());
             
        }

        public static StockMarketDTO Get(int id)
        {
            var repo = DataAccessFactory.StockMarketData();

            var Product = repo.Get(id);
            var ret = GetMapper().Map<StockMarketDTO>(Product);
            return ret;

        }

        public static StockMarketDTO Create(StockMarketDTO j)
        {
            j.date = DateTime.Now.ToString("dd-MM-yy");

            var repo = DataAccessFactory.StockMarketData();
            var finalData = GetMapper().Map<stock_market_data>(j);
            repo.Create(finalData);
            return j;

        }

        public static StockMarketDTO Update(StockMarketDTO j,int id)
        {
            var repo = DataAccessFactory.StockMarketData();
            var finalData = GetMapper().Map<stock_market_data>(j);
            repo.Update(finalData,id);
            return j;
        }

        public static bool Delete(int id)
        {
            var repo = DataAccessFactory.StockMarketData();
            repo.Delete(id);
            return true;
        }


        public static List<StockMarketDTO> Search(string tradeCode)
        {
            var stocks = Get(); // Fetch all stock data

            // Ensure tradeCode is not null or empty before searching
            if (!string.IsNullOrEmpty(tradeCode))
            {
                tradeCode = tradeCode.ToLower().Trim(); // Normalize input

                stocks = stocks
                    .Where(stock => stock.trade_code != null && stock.trade_code.ToLower().Contains(tradeCode))
                    .ToList();
            }

            return stocks; // Return filtered results or full list if no filter
        }

        public static void ExportApplicationsToCSV(string filePath)
        {
            var repo = DataAccessFactory.StockMarketData();
            repo.ExportApplicationsToCSV(filePath);
        }


    }
}
