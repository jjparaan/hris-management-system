using application.API.Entities.Common;

namespace application.API.Entities
{
    public class Department : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = [];
    }

}
