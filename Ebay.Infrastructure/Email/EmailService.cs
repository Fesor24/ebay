using Ebay.Application.Abstractions.Email;

namespace Ebay.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    public async Task SendAsync(string email, string body, string subject)
    {
        Console.WriteLine($"An email sent to @{email}. Subject: {subject}; Body: {body}");

        await Task.CompletedTask;
    }
}
