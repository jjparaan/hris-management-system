using application.Server.Contexts;
using application.Server.Services.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace application.Server.Services.Implementations.ApplicationLogs
{
    public class LogProcessingService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly LogQueue _logQueue;

        public LogProcessingService(IServiceProvider serviceProvider, LogQueue logQueue)
        {
            _serviceProvider = serviceProvider;
            _logQueue = logQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Wait for available logs in the queue
                await _logQueue.Lock.WaitAsync(stoppingToken);

                if (_logQueue.Queue.Count > 0)
                {
                    var log = _logQueue.Queue.Dequeue();

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        //context.ApplicationLogs.Add(log);
                        await context.SaveChangesAsync(stoppingToken);
                    }
                }

                _logQueue.Lock.Release();
                await Task.Delay(1000, stoppingToken); // Delay before checking again
            }
        }
    }
}
