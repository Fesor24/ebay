using Ebay.Application.Abstractions.Messaging;

namespace Ebay.Application.Auth.Login;

public sealed record LoginCommand(string Username, string Password) : 
    ICommand<LoginResponse>;
