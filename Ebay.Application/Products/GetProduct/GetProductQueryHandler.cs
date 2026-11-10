using Dapper;
using Ebay.Application.Abstractions.Data;
using Ebay.Application.Abstractions.Messaging;
using Ebay.Domain.Abstractions;
using Ebay.Domain.Products;

namespace Ebay.Application.Products.GetProduct;

internal sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetProductQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        string sql = """
            SELECT
                Id,
                Name,
                Description,
                money_amount AS Price,
                money_currency AS Currency
            FROM prd.Products
            WHERE Id = @ProductId

            """;

        var product = await connection.QueryFirstOrDefaultAsync<ProductResponse>(
            sql,
            new
            {
                request.ProductId
            }
            );

        return product is null ? 
            Result.Failure<ProductResponse>(ProductError.NotFound) : 
            product;
    }
}
