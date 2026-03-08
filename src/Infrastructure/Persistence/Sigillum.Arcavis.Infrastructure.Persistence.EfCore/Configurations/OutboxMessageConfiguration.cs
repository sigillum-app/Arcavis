using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Outbox;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OUTBOX_MESSAGE");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ID").HasColumnOrder(0);
        builder.Property<Guid?>("CREATED_BY").HasColumnOrder(1);
        builder.Property<DateTime>("CREATED_AT").HasColumnOrder(2);
        builder.Property<Guid?>("UPDATED_BY").HasColumnOrder(3);
        builder.Property<DateTime>("UPDATED_AT").HasColumnOrder(4);
        builder.Property<bool>("IS_DELETED").HasDefaultValue(false).HasColumnOrder(5);
        builder.Property(x => x.Type).HasColumnName("TYPE").HasColumnOrder(6);
        builder.Property(x => x.Payload).HasColumnName("PAYLOAD").HasColumnOrder(7);
        builder.Property(x => x.OccurredAt).HasColumnName("OCCURRED_AT").HasColumnOrder(8);
        builder.Property(x => x.ProcessedAt).HasColumnName("PROCESSED_AT").HasColumnOrder(9);
        builder.Property(x => x.Error).HasColumnName("ERROR").HasColumnOrder(10);
    }
}
