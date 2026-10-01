namespace Ebay.Domain.Abstractions;

public abstract class Entity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
}
