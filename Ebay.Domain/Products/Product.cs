using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Products;

public sealed class Product : AuditableEntity, ISoftDeleteable
{
    private Product() { }
    public Product(Guid id): base(id)
    {

    }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public List<string> Tags { get; private set; } = [];
    public ProductCondition Condition { get; private set; } = ProductCondition.Unknown;
    public Guid AssetId { get; private set; }
    public decimal PriceAmount { get; private set; }
    public decimal PriceCurrency { get; private set; }
    public string Location { get; private set;  }

    public bool IsDeleted { get; private set; } = false;
}
