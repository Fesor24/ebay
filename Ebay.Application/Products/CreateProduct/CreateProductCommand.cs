using Ebay.Application.Abstractions.Messaging;

namespace Ebay.Application.Products.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    List<string> Tags,
    decimal Price
    ) : 
    ICommand<Guid>;
