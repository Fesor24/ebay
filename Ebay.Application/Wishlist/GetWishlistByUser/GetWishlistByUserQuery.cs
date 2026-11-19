using Ebay.Application.Abstractions.Messaging;

namespace Ebay.Application.Wishlist.GetWishlistByUser;

public sealed record GetWishlistByUserQuery(
    Guid UserId
    ) : IQuery<IReadOnlyList<GetWishlistByUserResponse>>;
