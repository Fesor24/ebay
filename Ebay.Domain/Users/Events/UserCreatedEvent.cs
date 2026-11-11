using MediatR;

namespace Ebay.Domain.Users.Events;

public sealed record UserCreatedEvent(Guid UserId) : INotification;
