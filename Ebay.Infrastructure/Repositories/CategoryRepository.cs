using Ebay.Domain.Products;

namespace Ebay.Infrastructure.Repositories;

internal sealed class CategoryRepository(ApplicationDbContext dbContext) : 
    Repository<Category>(dbContext), ICategoryRepository
{
}
