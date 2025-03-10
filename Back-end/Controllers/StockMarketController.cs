using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Back_end.Controllers
{
    [EnableCors("*", "*", "*")]
    public class StockMarketController : ApiController
    {
        [HttpGet]
        [Route("api/page")]
        public HttpResponseMessage Get(int page = 1, int pageSize = 50)
        {
            var data = StockMarketService.Get(page, pageSize);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("api/all")]
        public HttpResponseMessage Get()
        {
            var data = StockMarketService.Get();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("api/stock/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var data = StockMarketService.Get(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }


        [HttpPost]
        [Route("api/stock/create")]
        public HttpResponseMessage Create(StockMarketDTO j)
        {
            StockMarketService.Create(j);
            return Request.CreateResponse(HttpStatusCode.OK, "Successfully Created!");
        }

        [HttpPut]
        [Route("api/stock/update/{id}")]
        public HttpResponseMessage Update(StockMarketDTO j,int id)
        {

            var updatedCandidate = StockMarketService.Update(j,id);
            return Request.CreateResponse(HttpStatusCode.OK, updatedCandidate);
        }

        [HttpDelete]
        [Route("api/stock/delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var deletedCandidate = StockMarketService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK, deletedCandidate);
        }

        [HttpGet]
        [Route("api/stock/search/{tradeCode}")]
        public HttpResponseMessage Search([FromUri] string tradeCode = null)
        {
            // Call to your service method to search for stock data
            var data = StockMarketService.Search(tradeCode);

            // Return the filtered results with OK status
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }


        [HttpPost]
        [Route("api/stock/export")]
        public HttpResponseMessage ExportApplicationsToCSV([FromBody] PathDTO path)
        {
            try
            {
                // Ensure the provided path is not null or empty
                if (string.IsNullOrEmpty(path?.Path))  // Corrected null check
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid path provided.");
                }

                // Create the full file path
                string filePath = Path.Combine(path.Path, "stock_market_data.csv");

                // Validate if the directory exists
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "The specified directory does not exist.");
                }

                // Call the service to export the CSV
                StockMarketService.ExportApplicationsToCSV(filePath);

                return Request.CreateResponse(HttpStatusCode.OK, "CSV exported successfully to " + filePath);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occurred: " + ex.Message);
            }
        }


    }
}
