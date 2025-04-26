namespace application.Server.Entities.Common
{
    public class AuditableBaseEntity : BaseEntity
    {
        public DateTime? LastModifiedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
