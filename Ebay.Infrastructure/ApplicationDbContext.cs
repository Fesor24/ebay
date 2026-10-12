using Ebay.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Ebay.Infrastructure;

internal sealed class ApplicationDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
