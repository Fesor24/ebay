using Ebay.Domain.Products;

namespace Ebay.Infrastructure.Repositories;

internal sealed class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}
