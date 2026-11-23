using Ebay.Application.Abstractions.Messaging;

namespace Ebay.Application.Cart.GetShoppingCart;

public sealed record GetShoppingCartQuery(Guid CartId) : 
    IQuery<ShoppingCartResponse>;
