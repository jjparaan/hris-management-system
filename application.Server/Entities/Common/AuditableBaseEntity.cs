namespace application.Server.Entities.Common
{
    public class AuditableBaseEntity : BaseEntity
    {
        public DateTime? LastModifiedOn { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
