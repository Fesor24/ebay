using Ebay.Application.Abstractions.Messaging;
using Ebay.Application.Cart.GetShoppingCart;

namespace Ebay.Application.Cart.AddUpdateShoppingCart;

public sealed record AddUpdateShoppingCartCommand(
    Guid CartId,
    List<ShoppingCartItemResponse> CartItems
    ) : ICommand<ShoppingCartResponse>;
