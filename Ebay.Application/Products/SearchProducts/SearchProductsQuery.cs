using Ebay.Application.Abstractions.Messaging;
using Ebay.Application.Products.GetProduct;

namespace Ebay.Application.Products.SearchProducts;

public sealed record SearchProductsQuery(
    string Name,
    string Location,
    decimal MinPrice,
    decimal MaxPrice
    ) : IQuery<IReadOnlyList<ProductResponse>>;
