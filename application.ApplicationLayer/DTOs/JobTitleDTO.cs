
using application.Domain.Entities;
using application.Domain.Common;

namespace application.ApplicationLayer.DTOs
{
    public class JobTitleDTO : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = [];
    }
}
