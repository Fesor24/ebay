using Ebay.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Ebay.Infrastructure.Repositories;

internal abstract class Repository<TEntity> where TEntity : Entity
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await DbContext.Set<TEntity>()
        .FirstOrDefaultAsync(ent => ent.Id == id, cancellationToken);

    public void Add(TEntity entity) => 
        DbContext.Add(entity);
}
