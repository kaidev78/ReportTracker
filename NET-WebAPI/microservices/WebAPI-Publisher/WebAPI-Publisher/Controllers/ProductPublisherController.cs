using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using WebAPI_ProductService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_Publisher.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductPublisherController : Controller
    {

        readonly IPublishEndpoint _publishEndpoint;
        private readonly IConfiguration _configuration;

        public ProductPublisherController(
            IPublishEndpoint publishEndpoint,
            IConfiguration configuration
            ) {
            _publishEndpoint = publishEndpoint;
            _configuration = configuration;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            // token is prefixed with "bearer"
            string token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
            ProductCreation productCreation = new ProductCreation {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Token = token
            };
            await _publishEndpoint.Publish(productCreation);
            return Ok("creation request sucessfully sent");
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
    }
}
