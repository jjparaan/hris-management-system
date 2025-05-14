using application.Domain.Common;

namespace application.Domain.Entities
{
    public class JobTitle : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = [];
    }

}
