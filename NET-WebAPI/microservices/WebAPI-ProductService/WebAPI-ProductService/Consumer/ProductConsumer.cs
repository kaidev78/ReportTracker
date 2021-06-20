using System;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using WebAPI_ProductService.Database;
using WebAPI_ProductService.Models;

namespace WebAPI_ProductService.Consumer
{
    public class ProductConsumer : IConsumer<ProductCreation>
    {
        private readonly IConfiguration _configuration;
        private readonly ProductDBOperation _databaseOperation;

        public ProductConsumer(IConfiguration configuration) {
            _configuration = configuration;
            _databaseOperation = new ProductDBOperation(_configuration.GetConnectionString("ReportTrackerDBCon"),
                _configuration.GetConnectionString("ReportTrackerAuthDBCon"));
        }

        public async Task Consume(ConsumeContext<ProductCreation> context)
        {
            string token = context.Message.Token;
            string redisDataSource = _configuration.GetConnectionString("ReportTrackerRedisCon");
            ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect(redisDataSource);
            IDatabase conn = muxer.GetDatabase();
            string redisRecord = conn.StringGet(token);
            int developerid;
            string developername;
            if (redisRecord != null)
            {
                Console.WriteLine("redis record is " + redisRecord);
                string[] userinfo = redisRecord.Split(',');
                developerid = Int32.Parse(userinfo[2]);
                developername = userinfo[0];
            }
            else
            {
                Console.WriteLine("not cached");
                var handler = new JwtSecurityTokenHandler();
                Console.WriteLine("token is " + token);
                var jsonToken = handler.ReadJwtToken(token);
                var tokenS = jsonToken as JwtSecurityToken;
                var getUserName = tokenS.Claims.First(claim => claim.Type == "unique_name").Value;
                Console.WriteLine("username is " + getUserName);
                DataTable dataTable = _databaseOperation.getUserInfo(getUserName);
                developerid = (int)dataTable.Rows[0]["UserId"];
                developername = (string)dataTable.Rows[0]["UserName"];
            }
            Product newProduct = new Product {
                ProductName = context.Message.ProductName,
                ProductDescription = context.Message.ProductDescription,
                DeveloperId = developerid,
                DeveloperName = developername
            };

            _databaseOperation.createProduct(newProduct);
            await Console.Out.WriteLineAsync("new product created, developer id: " + newProduct.DeveloperId
                + " product name: " + newProduct.ProductName + " product description: " + newProduct.ProductDescription
                + " developer name: " + newProduct.DeveloperName);
        }
    }
}
