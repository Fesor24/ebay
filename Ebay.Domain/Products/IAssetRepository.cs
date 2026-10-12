using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Products;

public interface IAssetRepository : IScopedService
{
    Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Add(Asset product);
}
