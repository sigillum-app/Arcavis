using RepoDb;
using Sigillum.Arcavis.Core.Application.Abstraction.ROM.Outbox.Base;

namespace Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.Mapping;

internal static class OutboxRomMap
{
    internal static void Configure()
    {
        FluentMapper
            .Entity<OutboxRom>()
            .Table("OUTBOX_MESSAGE")
            .Column(x => x.Id, "ID")
            .Column(x => x.Type, "TYPE")
            .Column(x => x.Payload, "PAYLOAD")
            .Column(x => x.OccurredAt, "OCCURRED_AT")
            .Column(x => x.ProcessedAt, "PROCESSED_AT")
            .Column(x => x.Error, "ERROR")
            .Column(x => x.RetryCount, "RETRY_COUNT")
            .Column(x => x.MaxRetryCount, "MAX_RETRY_COUNT")
            .Column(x => x.NextRetryAt, "NEXT_RETRY_AT");
    }
}

