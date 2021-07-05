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
using WebAPI_ProductService.Database;
using System.IdentityModel.Tokens.Jwt;
using WebAPI_ProductService.Enum;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_ProductService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IBusControl _bus;
        private readonly IConfiguration _configuration;
        private readonly ProductDBOperation _databaseOperation;

        public ProductController(
            IBusControl bus,
            IConfiguration configuration
            ) {
            _bus = bus;
            _configuration = configuration;
            _databaseOperation = new ProductDBOperation(_configuration.GetConnectionString("ReportTrackerDBCon"),
    _configuration.GetConnectionString("ReportTrackerAuthDBCon"));
        }

        // GET: api/values
        [HttpGet("{productId}")]
        public IActionResult GetProduct(int productId)
        {
            Console.WriteLine(productId);
            return Ok(_databaseOperation.getProduct(productId));
        }

        [HttpGet("/search/{searchName}")]
        public IActionResult searchProduct(string searchName)
        {
            Console.WriteLine("user is searching " + searchName);
            return Ok(_databaseOperation.getSearchResultsByName(searchName));
        }


        [HttpGet("/product-list")]
        public IActionResult GetProducts()
        {
            string token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var getUserName = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
            return Ok(_databaseOperation.getProducts(getUserName));
        }


        [HttpGet("/product-issues/{productId}")]
        public IActionResult GetProductIssues(int productId) {
            return Ok(_databaseOperation.getProductIssue(productId));
        }

        [HttpGet("/product-issue/{issueId}")]
        public IActionResult GetProductIssue(int issueId)
        {
            return Ok(_databaseOperation.getIssue(issueId));
        }

        [HttpDelete("/delete-issue/{issueId}")]
        public IActionResult DeleteIssue(int issueId) {
            string token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            int accountType = Int32.Parse(tokenS.Claims.First(claim => claim.Type == "AccountType").Value);
            if (accountType != (int)AccountType.ADMIN) {
                Console.WriteLine("This is not an admin");
                return Unauthorized("This is not an admin");
            }
            _databaseOperation.deleteIssue(issueId);
            return Ok("Sucessfully Deleted");
        }

        [HttpPost("/update-issue-status")]
        public IActionResult UpdateIssueStatus([FromBody]IssueStatus issueStatus) {
            JsonResult result = _databaseOperation.updateIssueStatus(issueStatus.IssueId, issueStatus.IssueStatusCode);
            return Ok(result);
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
