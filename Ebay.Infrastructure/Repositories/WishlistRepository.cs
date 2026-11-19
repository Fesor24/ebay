using Ebay.Domain.Wishlist;
using Microsoft.EntityFrameworkCore;

namespace Ebay.Infrastructure.Repositories;

internal sealed class WishlistRepository(ApplicationDbContext dbContext) : Repository<Wishlist>(dbContext), 
    IWishlistRepository
{
    private readonly ApplicationDbContext _dbContext;
    public async Task<IReadOnlyList<Wishlist>> GetAllByUserId(Guid userId)
    {
        return await _dbContext.Set<Wishlist>()
            .Where(w => w.UserId == userId)
            .ToListAsync();
    }
}
