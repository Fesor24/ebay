using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Users;

public interface IRoleRepository : IScopedService
{
    Task<Role?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<Role?> GetByIdAsync(Guid roleId, CancellationToken cancellationToken = default);
}
