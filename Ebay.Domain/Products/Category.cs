using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Products;

public sealed class Category : Entity
{
    public Category(Guid id) : base(id) { }

    public string Name { get; private set; }
}
