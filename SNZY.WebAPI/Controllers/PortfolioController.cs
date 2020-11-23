using Microsoft.AspNet.Identity;
using SNZY.Models.Portfolio;
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

        [Route ("")]
        public IHttpActionResult Post(PortfolioCreate portfolio)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePortfolioService();

            if (!service.CreatePortfolio(portfolio))
                return InternalServerError();

            return Ok();
        }

        [Route ("")]
        public IHttpActionResult Get()
        {
            PortfolioService portService = CreatePortfolioService();
            var posts = portService.GetPortfolio();
            return Ok(posts);
        }
    }
}
