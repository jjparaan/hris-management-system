using System;
using System.Threading.Tasks;

namespace application.API.Services.Interfaces.ApplicationLogs
{
    public interface ILogService
    {
        Task LogInformationAsync(string message, string method);
        Task LogWarningAsync(string message, string method);
        Task LogErrorAsync(string message, string method = "Server", Exception? ex = null);
    }
}
