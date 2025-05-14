using application.Domain.Common;
using application.Domain.Enums.Employee;

namespace application.Domain.Entities
{
    public class Employee : AuditableBaseEntity
    {
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string ShortCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; } // Foreign key to Department entity
        public virtual Department Department { get; set; }
        public int JobTitleId { get; set; } // Foreign key to JobTitle entity
        public virtual JobTitle JobTitle { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; } 
        public EmployeeStatus EmploymentStatus { get; set; } 
        public string ProfilePictureUrl { get; set; }
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = [];
        public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; } = [];
        public virtual ICollection<Payroll> Payrolls { get; set; } = [];

    }
}
