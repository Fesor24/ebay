namespace Ebay.Application.Products.GetProduct;

public sealed record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Location,
    string Currency,
    DateTime CreatedAt
    );
