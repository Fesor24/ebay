using Ebay.Domain.Users;

namespace Ebay.Application.Abstractions.Security;

public interface ITokenService
{
    (string Token, DateTime ExpiresAt) GenerateToken(User user);
}
