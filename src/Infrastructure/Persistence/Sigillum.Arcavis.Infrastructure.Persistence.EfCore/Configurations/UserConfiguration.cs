using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sigillum.Arcavis.Core.Domain.Entities;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations.Base;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations;

public class UserConfiguration : BaseConfiguration<UserEntity>
{
    public override void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("USER");

        builder.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
    }
}
