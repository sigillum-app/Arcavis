using Npgsql;
using System.Data;

namespace Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.Connection;

public sealed class ArcavisConnectionFactory
{
    private readonly string _connectionString;

    public ArcavisConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection Create()
    {
        return new NpgsqlConnection(_connectionString);
    }
}