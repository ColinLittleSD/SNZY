using Microsoft.AspNet.Identity;
using SNZY.Models.ETF;
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
    public class ETFController : ApiController
    {
        private ETFService CreateETFService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var etfService = new ETFService(userId);
            return etfService;
        }

        public IHttpActionResult Get()
        {
            var service = CreateETFService();
            var etfs = service.GetETFs();
            return Ok(etfs);
        }

        public IHttpActionResult Post(ETFCreate etf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateETFService();

            if (!service.CreateETF(etf))
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}
