using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Publisher.Models;
using StackExchange.Redis;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_Publisher.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IBusControl _bus;

        public ProductController(IBusControl bus) {
            _bus = bus;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            Uri uri = new Uri("rabbitmq://localhost/hello-world-queue");

            await Console.Out.WriteLineAsync(product.ProductName);
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(product);

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

        [HttpGet("/cache")]
        public void GetCache()
        {
            ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase conn = muxer.GetDatabase();
            var value = conn.StringGet("test");
            Console.WriteLine(value);
            muxer.Close();
        }
    }
}
