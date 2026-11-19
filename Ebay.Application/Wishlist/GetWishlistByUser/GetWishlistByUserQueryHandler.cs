using System.Data;
using Dapper;
using Ebay.Application.Abstractions.Data;
using Ebay.Application.Abstractions.Messaging;
using Ebay.Domain.Abstractions;

namespace Ebay.Application.Wishlist.GetWishlistByUser;

internal sealed class GetWishlistByUserQueryHandler(ISqlConnectionFactory connection) : IQueryHandler<GetWishlistByUserQuery, IReadOnlyList<GetWishlistByUserResponse>>
{
    private readonly IDbConnection _connection = connection.CreateConnection();

    public async Task<Result<IReadOnlyList<GetWishlistByUserResponse>>> Handle(GetWishlistByUserQuery request, 
        CancellationToken cancellationToken)
    {
        string query = @"SELECT p.id as ProductId, 
                            p.name as ProductName, 
                            p.money_amount as Price
                        FROM usr.wishlists w
                        JOIN prd.products p ON w.product_id = p.id
                        WHERE w.user_id = @UserId";

        var result = await _connection.QueryAsync<GetWishlistByUserResponse>(
            query, 
            new { 
                request.UserId 
            });

        return result.ToList();
    }
}
