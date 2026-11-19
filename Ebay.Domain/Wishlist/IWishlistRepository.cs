using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Wishlist;

public interface IWishlistRepository : IScopedService
{
    void Add(Wishlist wishlist);
    Task<Wishlist?> GetByIdAsync(Guid wishlistId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Wishlist>> GetAllByUserId(Guid userId);
}
