using application.Server.Entities.Common;
using application.Server.Entities.Enums.Employee;

namespace application.Server.Entities
{
    public class AttendanceRecord : AuditableBaseEntity
    {
        public int EmployeeId { get; set; } // Foreign key to Employee entity
        public virtual Employee Employee { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public AttendanceStatus Status { get; set; } 
    }

}
