using application.API.Contexts;
using application.Domain.Common;
using Serilog;
using System.Security.Claims;
using application.API.Services.Interfaces.ApplicationLogs;
using application.API.Services.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using application.Domain.Enums.ApplicationLogs;
using application.Domain.Entities;

namespace application.API.Services.Implementations.ApplicationLogs
{
    public class LogService : ILogService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly LogQueue _logQueue;

        public LogService(ApplicationDbContext context, IHttpContextAccessor contextAccessor, LogQueue logQueue)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _logQueue = logQueue;
        }

        public async Task LogInformationAsync(string message, string method)
        {
            Log.Information(message);
            await SaveLogToDatabase(ApplicationLogLevel.Information, message, method);
        }

        public async Task LogWarningAsync(string message, string method)
        {
            Log.Warning(message);
            await SaveLogToDatabase(ApplicationLogLevel.Warning, message, method);
        }

        public async Task LogErrorAsync(string message, string method, Exception? ex = null)
        {
            Log.Error(ex, message);
            await SaveLogToDatabase(ApplicationLogLevel.Error, message, method, ex);
        }

        private async Task SaveLogToDatabase(ApplicationLogLevel level, string message, string method, Exception? ex = null)
        {
            var logEntry = new ApplicationLog
            {
                Level = level,
                Message = message,
                Exception = ex?.ToString(),
                Logger = method,
                CreatedOn = DateTime.UtcNow,
                UserAccountId = GetCurrentUserId()
            };

            await _logQueue.Lock.WaitAsync();
            _logQueue.Queue.Enqueue(logEntry);
            _logQueue.Lock.Release();
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 1;
        }
    }
}
