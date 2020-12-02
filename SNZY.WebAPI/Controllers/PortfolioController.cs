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
using System.Threading.Tasks;
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

        /// <summary>
        /// Post a new portfolio
        /// </summary>
        /// <param name="portfolio">Portfolio name</param>
        /// <returns></returns>
        [Route("")]
        public async Task<IHttpActionResult> Post(PortfolioCreate portfolio)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePortfolioService();

            if (await service.CreatePortfolio(portfolio) == false)
                return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Get Portfolio
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            PortfolioService portService = CreatePortfolioService();
            var posts = await portService.GetPortfolio();
            return Ok(posts);
        }

        /// <summary>
        /// Get list of stocks inside the portfolio
        /// </summary>
        /// <returns></returns>
        [Route("~/api/StockPortfolio/GetPortfolioStocks")]
        public async Task<IHttpActionResult> GetPortfolioStocks()
        {
            var service = CreateStockPortfolioService();
            var portstocks = await service.GetStockPortfolio();
            return Ok(portstocks);
        }

        /// <summary>
        /// Post a stock to stock list inside the portfolio
        /// </summary>
        /// <param name="port">Stock Id, Portfolio Id</param>
        /// <returns></returns>
        [Route("~/api/StockPortfolio/PostPortfolioStocks")]
        public async Task<IHttpActionResult> PostStockPortfolio(StockPortfolioCreate port)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateStockPortfolioService();

            if (await service.CreateStockPortfolio(port) == false)
            {
                return InternalServerError();
            }

            return Ok();
        }

        /// <summary>
        /// Get a list of ETFs inside the porfolio
        /// </summary>
        /// <returns></returns>
        [Route("~/api/ETFPortfolio/GetPortfolioETFs")]
        public async Task<IHttpActionResult> GetPortfolioETFs()
        {
            var service = CreateETFPortfolioService();
            var portstocks = await service.GetETFPortfolio();
            return Ok(portstocks);
        }

        /// <summary>
        /// Post an ETF to ETF list inside the portfolio
        /// </summary>
        /// <param name="etfPort">ETF Id and Portfolio Id</param>
        /// <returns></returns>
        [Route("~/api/ETFPortfolio/PostPortfolioETFs")]
        public async Task<IHttpActionResult> PostETFPortfolio(ETFPortfolioCreate etfPort)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateETFPortfolioService();

            if (await service.CreateETFPortfolio(etfPort) == false)
            {
                return InternalServerError();
            }

            return Ok();
        }

        /// <summary>
        /// Remove an ETF from ETF list inside the portfolio
        /// </summary>
        /// <param name="id">The ETF Id to delete</param>
        /// <returns></returns>
        [Route("~/api/ETFPortfolio/RemovePortfolioETFs/{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveETFFromPortfolio(int id)
        {
            var service = CreateETFPortfolioService();
            if (await service.RemovePortfolioETFs(id) == false)
            {
                return InternalServerError();
            }
            return Ok();
        }

        /// <summary>
        /// Remove a Stock from Stock list inside the portfolio
        /// </summary>
        /// <param name="id">The stock Id to delete</param>
        /// <returns></returns>
        [Route("~/api/StockPortfolio/RemovePortfolioStocks/{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> RemoveStocksFromPortfolio(int id)
        {
            var service = CreateStockPortfolioService();
            if (await service.RemovePortfolioStocks(id) == false)
            {
                return InternalServerError();
            }
            return Ok();
        }
    }
}
