using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations.Base;

internal static class AuditConfiguration
{
    public static void Configure<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : class
    {
        builder.Property<DateTime>(AuditProperties.CreatedAt)
            .HasColumnName(AuditProperties.CreatedAt)
            .IsRequired()
            .HasColumnOrder(1);

        builder.Property<Guid?>(AuditProperties.CreatedBy)
            .HasColumnName(AuditProperties.CreatedBy)
            .HasColumnOrder(2);

        builder.Property<DateTime>(AuditProperties.UpdatedAt)
            .HasColumnName(AuditProperties.UpdatedAt)
            .IsRequired()
            .HasColumnOrder(3);

        builder.Property<Guid?>(AuditProperties.UpdatedBy)
            .HasColumnName(AuditProperties.UpdatedBy)
            .HasColumnOrder(4);
    }

    public static void Configure<TOwner, TOwned>(OwnedNavigationBuilder<TOwner, TOwned> builder) where TOwner : class where TOwned : class
    {
        builder.Property<DateTime>(AuditProperties.CreatedAt)
            .HasColumnName(AuditProperties.CreatedAt)
            .IsRequired()
            .HasColumnOrder(1);

        builder.Property<Guid?>(AuditProperties.CreatedBy)
            .HasColumnName(AuditProperties.CreatedBy)
            .HasColumnOrder(2);

        builder.Property<DateTime>(AuditProperties.UpdatedAt)
            .HasColumnName(AuditProperties.UpdatedAt)
            .IsRequired()
            .HasColumnOrder(3);

        builder.Property<Guid?>(AuditProperties.UpdatedBy)
            .HasColumnName(AuditProperties.UpdatedBy)
            .HasColumnOrder(4);
    }
}
