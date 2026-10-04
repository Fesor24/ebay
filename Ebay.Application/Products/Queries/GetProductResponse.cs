namespace Ebay.Application.Products.Queries;

internal sealed record GetProductResponse(
    Guid Id,
    string Name,
    string Description
    );
