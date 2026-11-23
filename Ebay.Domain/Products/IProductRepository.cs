using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Products;

public interface IProductRepository : IScopedService
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Product>>  GetProductsByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    void Add(Product product);
}
