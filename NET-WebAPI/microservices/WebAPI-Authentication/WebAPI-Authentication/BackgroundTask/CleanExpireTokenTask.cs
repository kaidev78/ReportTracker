using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAPI_Authentication.Database;

namespace WebAPI_Authentication.BackgroundTask
{

    public class CleanExpireTokenTask: IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger<CleanExpireTokenTask> _logger;
        private readonly IConfiguration _configuration;
        private readonly AuthDatabaseOperation _authDatabaseOperation;


        public CleanExpireTokenTask(ILogger<CleanExpireTokenTask> logger,
                                    IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _authDatabaseOperation = new AuthDatabaseOperation(_configuration.GetConnectionString("ReportTrackerAuthDBCon"));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            _logger.LogInformation("task started");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _authDatabaseOperation.cleanExpiredToken();
            _logger.LogInformation("working");
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
