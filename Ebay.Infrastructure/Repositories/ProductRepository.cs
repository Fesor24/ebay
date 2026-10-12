using Ebay.Domain.Products;

namespace Ebay.Infrastructure.Repositories;

internal abstract class ProductRepository : Repository<Product>, IProductRepository
{
    protected ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
