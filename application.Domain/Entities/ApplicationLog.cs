using application.Domain.Common;
using application.Domain.Enums.ApplicationLogs;

namespace application.Domain.Entities
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
