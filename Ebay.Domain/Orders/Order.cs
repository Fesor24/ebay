using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Orders;

public sealed class Order : AuditableEntity
{
    public string BuyerEmail { get; private set; }
    public OrderStatus Status { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }
    public DeliveryMethod DeliveryMethod { get; private set; }
    public Address ShippingAddress { get; private set; }
    public Decimal SubTotal => Items.Sum(x => x.GetProductInfo().Price);
    public Decimal Total => SubTotal + DeliveryMethod.Price;
    public ICollection<OrderItem> Items { get; private set; }

    public Order(Guid id,string street, string city, string state, string postCode,
        DeliveryMethod deliveryMethod, string buyerEmail) : base(id)
    {
        Status = OrderStatus.Pending;
        PaymentStatus = PaymentStatus.Paid;
        ShippingAddress = new Address(street, city, state, postCode);
        DeliveryMethod = deliveryMethod;
        BuyerEmail = buyerEmail;
        Items = [];
    }

    public void AddItem(Guid orderItemId, string productName, decimal price, Guid productId)
    {
        Items.Add(new OrderItem(orderItemId)
            .SetProductInfo(new ProductInfo(productName, price, productId)));
    }

    public static Order Create(Guid id, string street, string city, string state, string postCode,
        DeliveryMethod deliveryMethod, string buyerEmail)
    {
        return new Order(id, street, city, state, postCode, deliveryMethod, buyerEmail);
    }
}
