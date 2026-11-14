using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Products;

public interface ICategoryRepository : IScopedService
{
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
