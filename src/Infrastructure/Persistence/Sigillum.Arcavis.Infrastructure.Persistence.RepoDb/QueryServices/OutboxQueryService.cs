using RepoDb;
using Sigillum.Arcavis.Core.Application.Contracts.Persistence.QueryServices;
using Sigillum.Arcavis.Core.Application.Contracts.ROM.Outbox.Base;
using Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.Connection;

namespace Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.QueryServices;

internal sealed class OutboxQueryService : IOutboxQueryService
{
    private readonly ArcavisConnectionFactory _connectionFactory;

    public OutboxQueryService(ArcavisConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IReadOnlyList<OutboxRom>> GetUnprocessedMessagesAsync(int batchSize, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.Create();

        const string sql = @"
            SELECT 
                ""ID"", ""TYPE"", ""PAYLOAD"", ""OCCURRED_AT"", ""PROCESSED_AT"", 
                ""ERROR"", ""RETRY_COUNT"", ""MAX_RETRY_COUNT"", ""NEXT_RETRY_AT"" 
            FROM ""OUTBOX_MESSAGE"" 
            WHERE ""PROCESSED_AT"" IS NULL 
              AND ""IS_DELETED"" = FALSE
              AND (""NEXT_RETRY_AT"" IS NULL OR ""NEXT_RETRY_AT"" <= @Now)
              AND ""RETRY_COUNT"" < ""MAX_RETRY_COUNT""
            ORDER BY ""OCCURRED_AT"" ASC 
            LIMIT @BatchSize
            FOR UPDATE SKIP LOCKED";

        var result = await connection.ExecuteQueryAsync<OutboxRom>(
            sql,
            new { Now = DateTime.UtcNow, BatchSize = batchSize },
            cancellationToken: cancellationToken);

        return result.ToList().AsReadOnly();
    }
}
