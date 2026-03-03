using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sigillum.Arcavis.Core.Domain.Entities;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations.Base;

public class UserPasswordConfiguration : BaseConfiguration<UserPasswordEntity>
{
    public override void Configure(EntityTypeBuilder<UserPasswordEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("USER_PASSWORD");

        builder.Property(e => e.UserId).HasColumnName("USER_ID").IsRequired();
        builder.Property(e => e.PasswordHash).HasColumnName("PASSWORD_HASH").IsRequired();
    }
}
