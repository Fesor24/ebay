using System.Data;
using Ebay.Application.Abstractions.Data;
using Npgsql;

namespace Ebay.Infrastructure.Data;

internal class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    internal SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        return connection;
    }
}
