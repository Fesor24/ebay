using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Users;

public interface IUserRepository : IScopedService
{
    void Add(User user);
    Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
}
