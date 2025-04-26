using application.Server.Entities.Common;

namespace application.Server.Entities
{
    public class Log : BaseEntity
    {
        public LogLevel Level { get; set; } = LogLevel.Information;
        public string Message { get; set; }
        public string? Exception { get; set; } 
        public string? Logger { get; set; } 
        public int UserAccountId { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
