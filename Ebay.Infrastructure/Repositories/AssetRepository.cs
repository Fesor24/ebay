using Ebay.Domain.Products;

namespace Ebay.Infrastructure.Repositories;

internal abstract class AssetRepository : Repository<Asset>, IAssetRepository
{
    
    public AssetRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
