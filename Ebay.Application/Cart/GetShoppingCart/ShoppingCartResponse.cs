namespace Ebay.Application.Cart.GetShoppingCart;

public sealed record ShoppingCartResponse(
    string CartId,
    IReadOnlyList<ShoppingCartItemResponse> Items);

public sealed record ShoppingCartItemResponse(Guid ProductId, string ProductName, Decimal Price, int Quantity);
