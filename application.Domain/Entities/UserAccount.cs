using application.Domain.Common;
using application.Domain.Enums.UserAccount;

namespace application.Domain.Entities
{
    public class UserAccount : AuditableBaseEntity
    {
        public int EmployeeId { get; set; } // Foreign key to Employee entity
        public virtual Employee Employee { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; } 
        public virtual ICollection<ApplicationLog> ApplicationLogs { get; set; } = [];
    }
}
