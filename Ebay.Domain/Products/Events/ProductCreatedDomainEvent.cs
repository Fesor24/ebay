using Ebay.Domain.Abstractions;

namespace Ebay.Domain.Products.Events;

public sealed record ProductCreatedDomainEvent(
    Guid ProductId
    ) : IDomainEvent;
