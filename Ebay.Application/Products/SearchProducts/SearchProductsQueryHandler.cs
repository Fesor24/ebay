using Dapper;
using Ebay.Application.Abstractions.Data;
using Ebay.Application.Abstractions.Messaging;
using Ebay.Application.Products.CreateProduct;
using Ebay.Application.Products.GetProduct;
using Ebay.Domain.Abstractions;
using Ebay.Domain.Products;

namespace Ebay.Application.Products.SearchProducts;

internal sealed class SearchProductsQueryHandler : IQueryHandler<SearchProductsQuery, IReadOnlyList<ProductResponse>>
{
    //private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly ISearchService<ProductSearchSchema> _searchService;

    public SearchProductsQueryHandler(ISearchService<ProductSearchSchema> searchService)
    {
        _searchService = searchService;
    }
    public async Task<Result<IReadOnlyList<ProductResponse>>> Handle(SearchProductsQuery request, 
        CancellationToken cancellationToken)
    {
        decimal minPrice = request.MinPrice ?? 0;
        decimal maxPrice = request.MaxPrice ?? 0;

        if (request.MinPrice > request.MaxPrice) return new List<ProductResponse>();

        //using var connection = _sqlConnectionFactory.CreateConnection();

        //const string sql = """
        //    SELECT
        //        Id,
        //        Name,
        //        Description,
        //        Price,
        //        Location,
        //        Currency
        //    FROM prd.Products
        //    WHERE
        //        @MinPrice >= Price &&
        //        @MaxPrice <= Price

        //    """;

        //var products = await connection.QueryAsync<ProductResponse>(sql, new
        //{
        //    request.MaxPrice,
        //    request.MinPrice
        //});

        //return products.ToList();

        var (items, itemCount, page) = await _searchService.Search(request.Name, minPrice, maxPrice, cancellationToken);

        return items
            .Select(prd => new ProductResponse(
                Guid.Parse(prd.Id), 
                prd.Name, 
                prd.Description, 
                prd.Price, 
                $"{Currency.Gbp.Code}{prd.Price}",
                DateTime.UtcNow)).ToList();
    }
}
