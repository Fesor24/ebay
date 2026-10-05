using Ebay.Application.Abstractions.Email;
using Ebay.Domain.Products.Events;
using MediatR;

namespace Ebay.Application.Products.CreateProduct;

internal sealed class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent>
{
    private readonly IEmailService _emailService;
    public ProductCreatedDomainEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }
    public async Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        string message = "Product with ID: " + notification.ProductId + " created";

        await Task.Run(() => 
        Console.WriteLine(message), cancellationToken);

        await _emailService.SendAsync("fesor@dev.com", message, "Product Created");
    }
}
