using Ebay.Application.Abstractions.Messaging;

namespace Ebay.Application.Wishlist.CreateWishlist;

public sealed record CreateWishlistCommand(
    Guid ProductId,
    Guid UserId
    ) : ICommand<Guid>;
