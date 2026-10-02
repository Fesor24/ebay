namespace Ebay.Domain.Products;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Add(Product product);
}
