using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sigillum.Arcavis.Core.Domain.Users;
using Sigillum.Arcavis.Core.Domain.Users.Emails;
using Sigillum.Arcavis.Core.Domain.Users.Passwords;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations.Base;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations;

internal sealed class UserConfiguration : BaseConfiguration<User>
{
    private const string Emails = "_emails";
    private const string Passwords = "_passwords";

    protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("USER");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("ID")
            .HasConversion(
                id => id.Value,
                value => new UserId(value))
            .HasColumnOrder(0);

        builder.Property(x => x.IsActive)
            .HasColumnName("IS_ACTIVE")
            .HasColumnOrder(5);

        builder.Ignore(x => x.Emails);
        builder.Ignore(x => x.Passwords);

        ConfigureEmails(builder);
        ConfigurePasswords(builder);
    }

    private static void ConfigureEmails(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany<Email>(Emails, email =>
        {
            AuditConfiguration.Configure(email);

            email.ToTable("USER_EMAIL");

            email.HasKey(x => x.Id);

            email.Property(x => x.Id)
                .HasColumnName("ID")
                .HasConversion(
                    id => id.Value,
                    value => new EmailId(value))
                .HasColumnOrder(0);

            email.Property(x => x.EmailAddress)
                .HasColumnName("EMAIL")
                .IsRequired()
                .HasColumnOrder(5);

            email.Property(x => x.IsVerified)
                .HasColumnName("IS_VERIFIED")
                .HasColumnOrder(6);

            email.Property(x => x.StartAt)
                .HasColumnName("START_AT")
                .IsRequired()
                .HasColumnOrder(7);

            email.Property<Guid>("USER_ID")
                .HasColumnName("USER_ID")
                .HasColumnOrder(8);

            email.WithOwner()
                .HasForeignKey("USER_ID");

            email.HasIndex(x => x.EmailAddress)
                .IsUnique();
        });
    }

    private static void ConfigurePasswords(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany<Password>(Passwords, password =>
        {
            AuditConfiguration.Configure(password);

            password.ToTable("USER_PASSWORD");
            password.HasKey(x => x.Id);

            password.Property(x => x.Id)
                .HasColumnName("ID")
                .HasConversion(
                    id => id.Value,
                    value => new PasswordId(value))
                .HasColumnOrder(0);

            password.Property(x => x.PasswordHash)
                .HasColumnName("PASSWORD_HASH")
                .IsRequired()
                .HasColumnOrder(5);

            password.Property(x => x.StartAt)
                .HasColumnName("START_AT")
                .IsRequired()
                .HasColumnOrder(6);

            password.Property<Guid>("USER_ID")
                .HasColumnName("USER_ID")
                .HasColumnOrder(7);

            password.WithOwner()
                .HasForeignKey("USER_ID");
        });
    }
}