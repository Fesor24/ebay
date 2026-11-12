using Ebay.Domain.Abstractions;
using Ebay.Domain.Users;

namespace Ebay.Application.Abstractions.Security;

public interface ITokenService : IScopedService
{
    (string Token, DateTime ExpiresAt) GenerateToken(User user);
}
