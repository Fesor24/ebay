using Ebay.Application.Abstractions.Messaging;

namespace Ebay.Application.Users.GetUser;

public sealed record GetUserQuery(string Email) : IQuery<GetUserResponse>;
