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

        [HttpPost("/send-issue")]
        public async Task<IActionResult> SendIssue([FromBody] Issue issue)
        {
            await _publishEndpoint.Publish(issue);
            return Ok(new JsonResult("issue submission request sucessfully sent"));
        }
    }
}
