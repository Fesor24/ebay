using Ebay.Application.Abstractions.Messaging;

namespace Ebay.Application.Products.GetProduct;

public sealed record GetProductQuery(Guid ProductId) : IQuery<ProductResponse>;
