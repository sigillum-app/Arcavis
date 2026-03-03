using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sigillum.Arcavis.Core.Domain.Entities;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations.Base;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations;

public class UserEmailConfiguration : BaseConfiguration<UserEmailEntity>
{
    public override void Configure(EntityTypeBuilder<UserEmailEntity> builder)
    {
        base.Configure(builder);
        builder.ToTable("USER_EMAIL");

        builder.Property(e => e.UserId).HasColumnName("USER_ID").IsRequired();
        builder.Property(e => e.Email).HasColumnName("EMAIL").IsRequired();
        builder.Property(e => e.IsVerified).HasColumnName("IS_VERIFIED").IsRequired();
    }
}
