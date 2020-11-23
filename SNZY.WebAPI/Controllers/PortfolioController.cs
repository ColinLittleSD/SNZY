using Microsoft.AspNet.Identity;
using SNZY.Models.ETFPortfolio;
using SNZY.Models.Portfolio;
using SNZY.Models.StockPortfolio;
using SNZY.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SNZY.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Portfolio")]
    public class PortfolioController : ApiController
    {
        private PortfolioService CreatePortfolioService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var portService = new PortfolioService(userId);
            return portService;
        }

        private StockPortfolioService CreateStockPortfolioService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var stockPortfolioService = new StockPortfolioService(userId);
            return stockPortfolioService;
        }

        private ETFPortfolioService CreateETFPortfolioService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var etfPortfolioService = new ETFPortfolioService(userId);
            return etfPortfolioService;
        }

        [Route("")]
        public IHttpActionResult Post(PortfolioCreate portfolio)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePortfolioService();

            if (!service.CreatePortfolio(portfolio))
                return InternalServerError();

            return Ok();
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            PortfolioService portService = CreatePortfolioService();
            var posts = portService.GetPortfolio();
            return Ok(posts);
        }

        [Route("~/api/StockPortfolio/GetPortfolioStocks")]
        public IHttpActionResult GetPortfolioStocks()
        {
            var service = CreateStockPortfolioService();
            var portstocks = service.GetStockPortfolio();
            return Ok(portstocks);
        }

        [Route("~/api/StockPortfolio/PostPortfolioStocks")]
        public IHttpActionResult PostStockPortfolio(StockPortfolioCreate port)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateStockPortfolioService();

            if (!service.CreateStockPortfolio(port))
            {
                return InternalServerError();
            }

            return Ok();
        }

        [Route("~/api/ETFPortfolio/GetPortfolioETFs")]
        public IHttpActionResult GetPortfolioETFs()
        {
            var service = CreateETFPortfolioService();
            var portstocks = service.GetETFPortfolio();
            return Ok(portstocks);
        }

        [Route("~/api/ETFPortfolio/PostPortfolioETFs")]
        public IHttpActionResult PostETFPortfolio(ETFPortfolioCreate etfPort)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateETFPortfolioService();

            if (!service.CreateETFPortfolio(etfPort))
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}
