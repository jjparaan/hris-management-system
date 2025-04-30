using application.API.Entities.Common;
using application.API.Entities.Enums.ApplicationLogs;

namespace application.API.Entities
{
    public class ApplicationLog : BaseEntity
    {
        public ApplicationLogLevel Level { get; set; } = ApplicationLogLevel.Information;
        public string Message { get; set; }
        public string? Exception { get; set; } 
        public string? Logger { get; set; } 
        public int UserAccountId { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
