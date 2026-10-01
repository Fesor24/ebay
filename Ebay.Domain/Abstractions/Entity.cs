namespace Ebay.Domain.Abstractions;

public abstract class Entity
{
    private Entity() { }
    protected Entity(Guid id) => Id = id;
    public Guid Id { get; init; }
}
