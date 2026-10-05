using Ebay.Application.Abstractions.Messaging;

namespace Ebay.Application.Products.GetProduct;

internal sealed record GetProductQuery(Guid ProductId) : IQuery<ProductResponse>;
