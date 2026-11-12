using Ebay.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Ebay.Infrastructure.Repositories;

internal class RoleRepository(ApplicationDbContext dbContext) : Repository<Role>(dbContext), IRoleRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public Task<Role?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return _dbContext.Set<Role>()
            .FirstOrDefaultAsync(role => role.Code == code, cancellationToken);
    }
}
