using System.Text.Json;

namespace Ebay.Domain.Cart;

public sealed class ShoppingCart
{
    public string CartId { get; private set; }
    public List<ShoppingCartItem> Items { get; private set; }

    public ShoppingCart(Guid cartId)
    {
        CartId = cartId.ToString();
        Items = [];
    }

    public void AddItem(ShoppingCartItem item)
    {
        Items.Add(item);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public static ShoppingCart FromJson(string json)
    {
        return JsonSerializer.Deserialize<ShoppingCart>(json) ?? new ShoppingCart(Guid.NewGuid());
    }
}
