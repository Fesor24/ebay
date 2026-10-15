using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Products;

public sealed class Category : Entity
{
    public Category() { }
    public Category(Guid id) : base(id) { }

    public string Name { get; private set; }
    public ICollection<Product> Products { get; private set; } = [];
}
