using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Users;

public interface IUserRepository : IScopedService
{
    void Add(User user);
    Task<User?> GetByIdAsync(Guid userId);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
}
