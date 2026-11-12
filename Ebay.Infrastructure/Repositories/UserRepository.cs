using Ebay.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ebay.Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationDbContext dbContext, ILogger<UserRepository> logger) : Repository<User>(dbContext), IUserRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<User>()
            .FirstOrDefaultAsync(usr => usr.Email == email.ToLower(), cancellationToken);
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<User>()
            .Include(usr => usr.Role)
            .FirstOrDefaultAsync(usr => usr.Username == username.ToLower(), cancellationToken);
    }
}
