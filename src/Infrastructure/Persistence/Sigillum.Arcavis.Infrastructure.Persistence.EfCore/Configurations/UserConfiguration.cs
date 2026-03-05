using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sigillum.Arcavis.Core.Domain.Users;
using Sigillum.Arcavis.Core.Domain.Users.Emails;
using Sigillum.Arcavis.Core.Domain.Users.Passwords;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    private const string Emails = "_emails";
    private const string Passwords = "_passwords";

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("USER");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("ID").HasConversion(id => id.Value, value => new UserId(value)).HasColumnOrder(0);
        builder.Property<Guid?>("CREATED_BY").HasColumnOrder(1);
        builder.Property<DateTime>("CREATED_AT").HasColumnOrder(2);
        builder.Property<Guid?>("UPDATED_BY").HasColumnOrder(3);
        builder.Property<DateTime>("UPDATED_AT").HasColumnOrder(4);
        builder.Property<bool>("IS_DELETED").HasDefaultValue(false).HasColumnOrder(5);
        builder.Property(x => x.IsActive).HasColumnName("IS_ACTIVE");

        builder.Ignore(x => x.Emails);
        builder.Ignore(x => x.Passwords);

        ConfigureEmails(builder);
        ConfigurePasswords(builder);
    }

    private static void ConfigureEmails(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany<Email>(Emails, email =>
        {
            email.ToTable("USER_EMAIL");

            email.HasKey("Id");

            email.WithOwner().HasForeignKey("USER_ID");

            email.Property<EmailId>("Id").HasColumnName("ID").HasConversion(id => id.Value, value => new EmailId(value)).HasColumnOrder(0);
            email.Property<Guid?>("CREATED_BY").HasColumnOrder(1);
            email.Property<DateTime>("CREATED_AT").HasColumnOrder(2);
            email.Property<Guid?>("UPDATED_BY").HasColumnOrder(3);
            email.Property<DateTime>("UPDATED_AT").HasColumnOrder(4);
            email.Property<bool>("IS_DELETED").HasDefaultValue(false).HasColumnOrder(5);
            email.Property(x => x.EmailAddress).HasColumnName("EMAIL");
            email.Property(x => x.IsVerified).HasColumnName("IS_VERIFIED");
            
            email.HasIndex(x => x.EmailAddress).IsUnique();
        });
    }

    private static void ConfigurePasswords(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany<Password>(Passwords, password =>
        {
            password.ToTable("USER_PASSWORD");

            password.HasKey("Id");

            password.WithOwner().HasForeignKey("USER_ID");

            password.Property<PasswordId>("Id").HasColumnName("ID").HasConversion(id => id.Value, value => new PasswordId(value)).HasColumnOrder(0);
            password.Property<Guid?>("CREATED_BY").HasColumnOrder(1);
            password.Property<DateTime>("CREATED_AT").HasColumnOrder(2);
            password.Property<Guid?>("UPDATED_BY").HasColumnOrder(3);
            password.Property<DateTime>("UPDATED_AT").HasColumnOrder(4);
            password.Property<bool>("IS_DELETED").HasDefaultValue(false).HasColumnOrder(5);
            password.Property(x => x.PasswordHash).HasColumnName("PASSWORD_HASH");
        });
    }
}