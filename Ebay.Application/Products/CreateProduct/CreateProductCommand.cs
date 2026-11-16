using Ebay.Application.Abstractions.Messaging;
using FluentValidation;

namespace Ebay.Application.Products.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    List<string> Tags,
    decimal Price,
    Guid CategoryId,
    Guid UserId
    ) : 
    ICommand<Guid>;


