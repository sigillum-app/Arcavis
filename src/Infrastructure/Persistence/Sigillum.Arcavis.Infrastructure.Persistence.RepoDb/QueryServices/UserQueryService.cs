using RepoDb;
using Sigillum.Arcavis.Core.Application.Common.Pagination;
using Sigillum.Arcavis.Core.Application.Contracts.Persistence.QueryServices;
using Sigillum.Arcavis.Core.Application.ROMs.Users.Base;
using Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.Connection;

namespace Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.QueryServices;

internal sealed class UserQueryService : IUserQueryService
{
    private readonly ArcavisConnectionFactory _connectionFactory;

    public UserQueryService(ArcavisConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<UserRom?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.Create();
        return await connection.QueryAsync<UserRom>(
            e => e.Id == id,
            cancellationToken: cancellationToken)
            .ContinueWith(t => t.Result.FirstOrDefault(), cancellationToken);
    }

    public async Task<PagedResponse<UserRom>> GetPagedAsync(PaginationParams pagination, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.Create();

        var countSql = "SELECT COUNT(*) FROM \"USER\"";
        var totalCount = await connection.ExecuteScalarAsync<int>(countSql, cancellationToken: cancellationToken);

        var querySql = """
                         SELECT "ID"        AS "Id", 
                                "IS_ACTIVE" AS "IsActive"
                         FROM "USER"
                         ORDER BY "ID"
                         LIMIT @PageSize OFFSET @Offset
                       """;

        var items = await connection.ExecuteQueryAsync<UserRom>(
            querySql,
            new { pagination.PageSize, pagination.Offset },
            cancellationToken: cancellationToken);

        var totalPages = (int)Math.Ceiling((double)totalCount / pagination.PageSize);

        return new PagedResponse<UserRom>(
            Items: items,
            TotalCount: totalCount,
            TotalPages: totalPages,
            CurrentPage: pagination.Page,
            HasNextPage: pagination.Page < totalPages,
            HasPreviousPage: pagination.Page > 1
        );
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.Create();
        return await connection.ExistsAsync<UserRom>(
            e => e.Id == id,
            cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<UserRom>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.Create();

        var items = await connection.QueryAllAsync<UserRom>(cancellationToken: cancellationToken);

        return items.ToList().AsReadOnly();
    }
}
