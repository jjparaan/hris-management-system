using application.API.Entities.Common;
using application.API.Entities.Enums.Employee;

namespace application.API.Entities
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
