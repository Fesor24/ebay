namespace Ebay.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(string email, string body, string subject);
}
