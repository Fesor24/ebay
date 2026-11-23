using System;
using System.Collections.Generic;
using System.Text;

namespace Ebay.Domain.Cart;

public sealed class ShoppingCartItem
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; } = 1;
    public Decimal Price { get; private set; }  

    public ShoppingCartItem(Guid productId, string productName, Decimal price, int quantity)
    {
        ProductId = productId;
        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }
}
