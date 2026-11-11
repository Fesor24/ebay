

using Ebay.Application.Abstractions.Messaging;

namespace Ebay.Application.Users.CreateUser;

internal sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string Username,
    string Password,
    string Email,
    string PhoneNo
    ) : ICommand<string>;
