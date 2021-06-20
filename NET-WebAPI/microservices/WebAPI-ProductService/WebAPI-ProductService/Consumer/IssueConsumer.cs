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
    public class IssueConsumer: IConsumer<Issue>
    {
        private readonly IConfiguration _configuration;
        private readonly ProductDBOperation _databaseOperation;

        public IssueConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
            _databaseOperation = new ProductDBOperation(_configuration.GetConnectionString("ReportTrackerDBCon"),
                _configuration.GetConnectionString("ReportTrackerAuthDBCon"));
        }

        public async Task Consume(ConsumeContext<Issue> context)
        {
            _databaseOperation.createIssue(context.Message);
            await Console.Out.WriteLineAsync(context.Message.IssueName);
        }
    }
}
