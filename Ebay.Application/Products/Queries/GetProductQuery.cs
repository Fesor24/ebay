using Ebay.Application.Abstractions.Messaging;

namespace Ebay.Application.Products.Queries;

internal sealed record GetProductQuery(Guid ProductId) : IQuery<GetProductResponse>;
