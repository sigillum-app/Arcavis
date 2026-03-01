using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sigillum.Arcavis.Core.Domain.Entities.Base;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations.Base;

public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("ID").HasColumnOrder(0);

        #region Auditing
        builder.Property<Guid?>("CREATED_BY").HasColumnOrder(1);
        builder.Property<DateTime>("CREATED_AT").HasColumnOrder(2);
        builder.Property<Guid?>("UPDATED_BY").HasColumnOrder(3);
        builder.Property<DateTime>("UPDATED_AT").HasColumnOrder(4);
        #endregion

        #region Soft Delete
        builder.Property<bool>("IS_DELETED").HasDefaultValue(false).HasColumnOrder(5);

        builder.HasQueryFilter(e => EF.Property<bool>(e, "IS_DELETED") == false);
        #endregion
    }
}
