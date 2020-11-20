using Microsoft.AspNet.Identity;
using SNZY.Models;
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
    public class StockController : ApiController
    {
        private StockService CreateStockService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var postService = new StockService(userId);
            return postService;
        }

        public IHttpActionResult Post(StockCreate stock)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateStockService();

            if (!service.CreateStock(stock))
                return InternalServerError();

            return Ok();
        }
        public IHttpActionResult Get()
        {
            StockService stockService = CreateStockService();
            var posts = stockService.GetStockPosts();
            return Ok(posts);
        }
    }
}
