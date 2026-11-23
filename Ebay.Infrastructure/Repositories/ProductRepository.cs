using Ebay.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Ebay.Infrastructure.Repositories;

internal sealed class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Product>> GetProductsByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        return _dbContext.Set<Product>()
            .Where(p => ids.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }
}
