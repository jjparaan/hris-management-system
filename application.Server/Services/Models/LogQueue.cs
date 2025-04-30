using application.API.Entities;
using System.Collections.Generic;
using System.Threading;

namespace application.API.Services.Models
{
    public class LogQueue
    {
        public Queue<ApplicationLog> Queue { get; } = new Queue<ApplicationLog>();
        public SemaphoreSlim Lock { get; } = new SemaphoreSlim(1, 1); // For thread safety
    }

}
