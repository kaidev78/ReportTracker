using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI_ProductService.Models;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_ProductService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IBusControl _bus;
        private readonly IConfiguration _configuration;

        public ProductController(
            IBusControl bus,
            IConfiguration configuration
            ) {
            _bus = bus;
            _configuration = configuration;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]Product product)
        {

            
            return Ok(product.ProductName);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [AllowAnonymous]
        [HttpGet("/cache")]
        public void GetCache()
        {
            ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase conn = muxer.GetDatabase();
            var value = conn.StringGet("TEST");
            Console.WriteLine(value);
            muxer.Close();
        }
    }
}
