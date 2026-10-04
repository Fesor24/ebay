using Ebay.Application.Abstractions.Messaging;
using Ebay.Domain.Abstractions;

namespace Ebay.Application.Products.Queries;

internal sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, GetProductResponse>
{
    public Task<Result<GetProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
