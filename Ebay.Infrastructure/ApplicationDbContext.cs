using Ebay.Application.Exceptions;
using Ebay.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ebay.Infrastructure;

internal sealed class ApplicationDbContext(DbContextOptions options, IPublisher publisher) : 
    DbContext(options), IUnitOfWork
{
    private readonly IPublisher _publisher = publisher;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("A concurrency error occured while saving changes", ex);
        }
    }

    public async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
            await _publisher.Publish(domainEvent);
    }
}
