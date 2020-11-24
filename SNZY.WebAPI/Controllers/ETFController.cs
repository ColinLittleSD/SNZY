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
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var service = CreateETFService();
            var etfs = await service.GetETFs();
            return Ok(etfs);
        }

        //POST /api/ETF
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
        [Route("~/api/ETF/GetStocks")]
        public async Task<IHttpActionResult> GetETF_Stocks()
        {
            var service = CreateETF_StockService();
            var etfstocks = await service.GetETF_Stocks();
            return Ok(etfstocks);
        }

        //POST /api/ETF/PostStocks
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
