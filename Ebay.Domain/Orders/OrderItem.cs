using System.Text.Json;
using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Orders;

public sealed class OrderItem : Entity
{
    private OrderItem()
    {
        
    }

    public OrderItem(Guid id) : base(id)
    {
    }   
    public string ProductInfo { get; private set; }

    public OrderItem SetProductInfo(ProductInfo productInfo)
    {
        ProductInfo = productInfo.ToString();

        return this;
    }
    
    public ProductInfo GetProductInfo()
    {
        return JsonSerializer.Deserialize<ProductInfo>(ProductInfo)!;
    }
}

public record ProductInfo(string ProductName, Decimal Price, Guid ProductId)
{
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}   
