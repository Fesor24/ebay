

using Ebay.Application.Abstractions.Messaging;
using Ebay.Application.Auth.Login;

namespace Ebay.Application.Users.CreateUser;

public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string Username,
    string Password,
    string Email,
    string PhoneNo,
    Guid? RoleId
    ) : ICommand<LoginResponse>;
