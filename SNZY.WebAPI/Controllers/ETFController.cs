using Microsoft.AspNet.Identity;
using SNZY.Models;
using SNZY.Models.ETF;
using SNZY.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SNZY.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/ETF")]
    public class ETFController : ApiController
    {
        private ETFService CreateETFService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var etfService = new ETFService(userId);
            return etfService;
        }

        private ETF_StockService CreateETF_StockService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var etf_stockService = new ETF_StockService(userId);
            return etf_stockService;
        }

        //GET /api/ETF
        /// <summary>
        /// Get a list of ETFs
        /// </summary>
        /// <returns>List of ETFs</returns>
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var service = CreateETFService();
            var etfs = await service.GetETFs();
            return Ok(etfs);
        }

        //POST /api/ETF
        /// <summary>
        /// Post an ETF
        /// </summary>
        /// <param name="etf">ETF Name, Ticker</param>
        /// <returns></returns>
        [Route("")]
        public async Task<IHttpActionResult> Post(ETFCreate etf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateETFService();

            if (await service.CreateETF(etf) == false)
            {
                return InternalServerError();
            }

            return Ok();
        }

        //GET /api/ETF/GetStocks
        /// <summary>
        /// Get ETF Holdings "list of stocks".
        /// </summary>
        /// <returns>List of Stocks that ETF holds</returns>
        [Route("~/api/ETF/GetStocks")]
        public async Task<IHttpActionResult> GetETF_Stocks()
        {
            var service = CreateETF_StockService();
            var etfstocks = await service.GetETF_Stocks();
            return Ok(etfstocks);
        }

        //POST /api/ETF/PostStocks
        /// <summary>
        /// Post a stock to ETF Holdings "list of stocks"
        /// </summary>
        /// <param name="etf_stock">The stock Id and the ETF Id</param>
        /// <returns></returns>
        [Route("~/api/ETF/PostStocks")]
        public async Task<IHttpActionResult> PostETF_Stocks(ETF_StockCreate etf_stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateETF_StockService();

            if (await service.CreateETF_Stock(etf_stock) == false)
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}
