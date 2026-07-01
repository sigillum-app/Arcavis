using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations.Base;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Outbox;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations;

internal sealed class OutboxMessageConfiguration : BaseConfiguration<OutboxMessage>
{
    protected override void ConfigureEntity(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OUTBOX_MESSAGE");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ID").HasColumnOrder(0);
        builder.Property(x => x.Type).HasColumnName("TYPE").HasColumnOrder(5);
        builder.Property(x => x.Payload).HasColumnName("PAYLOAD").HasColumnOrder(6);
        builder.Property(x => x.OccurredAt).HasColumnName("OCCURRED_AT").HasColumnOrder(7);
        builder.Property(x => x.ProcessedAt).HasColumnName("PROCESSED_AT").HasColumnOrder(8);
        builder.Property(x => x.Error).HasColumnName("ERROR").HasColumnOrder(9);
        builder.Property(x => x.RetryCount).HasColumnName("RETRY_COUNT").HasColumnOrder(10);
        builder.Property(x => x.MaxRetryCount).HasColumnName("MAX_RETRY_COUNT").HasColumnOrder(11);
        builder.Property(x => x.NextRetryAt).HasColumnName("NEXT_RETRY_AT").HasColumnOrder(12);
    }
}
