using application.API.Entities.Common;
using application.API.Entities.Enums.UserAccount;

namespace application.API.Entities
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
