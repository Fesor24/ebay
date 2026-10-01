namespace Ebay.Domain.Abstractions;

public interface ISoftDeleteable
{
    bool IsDeleted { get; }
}
