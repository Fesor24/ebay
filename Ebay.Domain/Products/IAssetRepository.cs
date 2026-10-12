namespace Ebay.Domain.Products;

public interface IAssetRepository
{
    Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Add(Asset product);
}
