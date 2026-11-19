namespace Ebay.Application.Wishlist.GetWishlistByUser;

public sealed record GetWishlistByUserResponse(
    Guid ProductId,
    string ProductName,
    Decimal Price
    );
