using Ebay.Domain.Abstractions;

namespace Ebay.Application.Abstractions.Email;

public interface IEmailService : IScopedService
{
    Task SendAsync(string email, string body, string subject);
}
