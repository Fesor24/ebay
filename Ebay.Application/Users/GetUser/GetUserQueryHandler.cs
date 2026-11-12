using System.Data;
using Dapper;
using Ebay.Application.Abstractions.Data;
using Ebay.Application.Abstractions.Messaging;
using Ebay.Application.Exceptions;
using Ebay.Domain.Abstractions;

namespace Ebay.Application.Users.GetUser;

internal sealed class GetUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : 
    IQueryHandler<GetUserQuery, GetUserResponse>
{
    private readonly IDbConnection connection = sqlConnectionFactory.CreateConnection();
    public async Task<Result<GetUserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        const string query = """
                SELECT 
                    first_name AS FirstName,
                    last_name AS LastName,
                    username AS Username,
                    phone_no AS PhoneNo,
                    email AS Email
                FROM usr.users
                WHERE Email = @Email
            """;

        var result = await connection.QueryFirstOrDefaultAsync<GetUserResponse>(query, new
        {
            request.Email
        });

        return result ?? throw new NotFoundException("User not found");
    }
}
