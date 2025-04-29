using application.Server.Entities.Common;

namespace application.Server.Entities.DTOs
{
    public class JobTitleDTO : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = [];
    }
}
