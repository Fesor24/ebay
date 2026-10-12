using Ebay.Domain.Abstractions;
using Ebay.Domain.Products.Events;

namespace Ebay.Domain.Products;

public sealed class Product : AuditableEntity, ISoftDeleteable
{
    //private Product() { }
    public Product(Guid id): base(id)
    {

    }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ProductStatus Status { get; private set; }
    public List<string> Tags { get; private set; } = [];
    public ProductCondition Condition { get; private set; } = ProductCondition.Unknown;
    public Money Money { get; private set;  }
    public string Location { get; private set;  }

    public bool IsDeleted { get; private set; } = false;
    public ICollection<Asset> Assets { get; private set; } = [];

    public static Product Create(Guid productId, string name, string description,
        List<string> tags,string location, decimal price, string currency,
        DateTime createdAtUtc)
    {
        Product product = new(productId)
        {
            Status = ProductStatus.Listed,
            Name = name,
            Description = description,
            Tags = tags,
            Location = location,
            Money = new Money(price, Currency.Gbp)
        };

        product.SetAudits(createdAtUtc);

        product.RaiseDomainEvent(new ProductCreatedDomainEvent(product.Id));

        return product;
    }
}
