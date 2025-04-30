using application.API.Entities.Common;

namespace application.API.Entities
{
    public class Payroll : AuditableBaseEntity
    {
        public int EmployeeId { get; set; } // Foreign key to Employee entity
        public virtual Employee Employee { get; set; }
        public DateTime PayrollDate { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }
    }
}
