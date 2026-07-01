using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations.Base;

internal abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        AuditConfiguration.Configure(builder);

        ConfigureEntity(builder);
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}
