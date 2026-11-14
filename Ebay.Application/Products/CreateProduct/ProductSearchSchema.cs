namespace Ebay.Application.Products.CreateProduct;

public record ProductSearchSchema(
    string Id,
    string Name,
    string Description,
    Decimal Price,
    string Category
    );
