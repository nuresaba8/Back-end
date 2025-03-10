using DAL.EF;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class StockMarkeRepo : Repo, IRepo<stock_market_data, int, stock_market_data>
    {
        public stock_market_data Create(stock_market_data user)
        {
            user.date=DateTime.Now.Date;
            db.stock_market_data.Add(user);
            db.SaveChanges();
            return user;
        }

        public stock_market_data Get(int id)
        {
            return db.stock_market_data.Find(id);
        }


        public bool Delete(int id)
        {
            var ex = db.stock_market_data.Find(id);
            db.stock_market_data.Remove(ex);
            return db.SaveChanges() > 0;
        }

        public List<stock_market_data> Get(int page, int pageSize)
        {
            return db.stock_market_data
                .OrderByDescending(s => s.date)  // Sort by date (optional)
                .Skip((page - 1) * pageSize)      // Skip records based on page number
                .Take(pageSize)                   // Fetch only 'pageSize' records
                .ToList();
        }

        public List<stock_market_data> Get()
        {
            return db.stock_market_data.ToList();
        }


        public stock_market_data Update(stock_market_data obj, int id)
        {
            var ex = db.stock_market_data.Find(id);

            // Ensure you're not trying to modify the 'id' field
            ex.trade_code = obj.trade_code;
            ex.high = obj.high;
            ex.low = obj.low;
            ex.open = obj.open;
            ex.close = obj.close;
            ex.volume = obj.volume;


            // Save changes, but the 'id' field won't be modified
            db.SaveChanges();

            return ex;
        }


        public void ExportApplicationsToCSV(string filePath)
        {
            var stock = db.stock_market_data.ToList();

            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("JobId,Company,Position,AppliedDate,Status");

            foreach (var app in stock)
            {
                var line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                app.id,
                app.date,
                app.trade_code,
                app.high,
                app.low, app.open,
                app.close,app.volume);
                csvBuilder.AppendLine(line);
            }

            // Save the CSV content to the specified file path
            File.WriteAllText(filePath, csvBuilder.ToString());
        }

    }
}
