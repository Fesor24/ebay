namespace Ebay.Domain.Shared;

public class AuditableEntity
{
    public DateTime CreatedAtUtc {  get; private set; }
    public DateTime? UpdatedAtUtc { get; private set; }

    public virtual void SetAudits(DateTime createdAtUtc, DateTime? updatedAtUtc)
    {
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = updatedAtUtc;
    }
}
