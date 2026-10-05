using Dapper;
using Ebay.Application.Abstractions.Data;
using Ebay.Application.Abstractions.Messaging;
using Ebay.Application.Products.GetProduct;
using Ebay.Domain.Abstractions;

namespace Ebay.Application.Products.SearchProducts;

internal sealed class SearchProductsQueryHandler : IQueryHandler<SearchProductsQuery, IReadOnlyList<ProductResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchProductsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task<Result<IReadOnlyList<ProductResponse>>> Handle(SearchProductsQuery request, 
        CancellationToken cancellationToken)
    {
        if (request.MinPrice > request.MaxPrice) return new List<ProductResponse>();

        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                Id,
                Name,
                Description,
                Price,
                Location,
                Currency
            FROM prd.Products
            WHERE
                @MinPrice >= Price &&
                @MaxPrice <= Price

            """;

        var products = await connection.QueryAsync<ProductResponse>(sql, new
        {
            request.MaxPrice,
            request.MinPrice
        });

        return products.ToList();
    }
}
