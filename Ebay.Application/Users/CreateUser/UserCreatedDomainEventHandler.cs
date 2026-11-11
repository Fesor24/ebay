using Ebay.Domain.Users.Events;
using MediatR;

namespace Ebay.Application.Users.CreateUser;

internal sealed class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedEvent>
{
    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("User created with Id + " + notification.UserId.ToString());\c

        return Task.CompletedTask;
    }
}
