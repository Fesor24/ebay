using Ebay.Domain.Shared;

namespace Ebay.Domain.Products;

public sealed class Product : AuditableEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public List<string> Tags { get; private set; } = [];
    public ProductCondition Condition { get; private set; } = ProductCondition.Unknown;
    public Guid AssetId { get; private set; }
    public decimal Price { get; private set; }
    public string Location { get; private set;  }

}
