using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sigillum.Arcavis.Core.Domain.Entities.Base;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations.Base;

public abstract class TenantConfiguration<TEntity> : BaseConfiguration<TEntity> where TEntity : TenantEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.TenantId).HasColumnName("TENANT_ID").HasColumnOrder(6);
    }
}
