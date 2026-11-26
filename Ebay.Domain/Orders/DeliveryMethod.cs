using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Orders;

public sealed class DeliveryMethod : Entity
{
    public DeliveryMethod(Guid id, string name, decimal price) : base(id)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; private set; }
    public decimal Price { get; private set; }
}
