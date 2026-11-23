using Ebay.Application.Abstractions.Messaging;

namespace Ebay.Application.Cart.DeleteShoppingCart;

public sealed record DeleteShoppingCartCommand(Guid CartId) : ICommand<bool>;
