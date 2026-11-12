namespace Ebay.Application.Auth.Login;

public sealed record LoginResponse(string Token, DateTime ExpiresAt);
