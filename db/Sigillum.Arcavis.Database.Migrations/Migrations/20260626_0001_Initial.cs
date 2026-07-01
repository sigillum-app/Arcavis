using FluentMigrator;
using System.Data;

namespace Sigillum.Arcavis.Database.Migrations.Migrations;

[Migration(0001, "Initial_20260626")]
public sealed class Initial : Migration
{
    public override void Up()
    {
        CreateUsers();
        CreateUserEmails();
        CreateUserPasswords();
        CreateOutboxMessages();

        CreateIndexes();
        CreateForeignKeys();
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_USER_EMAIL_USER_USER_ID").OnTable("USER_EMAIL");
        Delete.ForeignKey("FK_USER_PASSWORD_USER_USER_ID").OnTable("USER_PASSWORD");

        Delete.Index("IX_USER_EMAIL_EMAIL").OnTable("USER_EMAIL");
        Delete.Index("IX_USER_EMAIL_USER_ID").OnTable("USER_EMAIL");
        Delete.Index("IX_USER_PASSWORD_USER_ID").OnTable("USER_PASSWORD");
        Delete.Index("IX_OUTBOX_MESSAGE_PROCESSED_AT").OnTable("OUTBOX_MESSAGE");
        Delete.Index("IX_OUTBOX_MESSAGE_NEXT_RETRY_AT").OnTable("OUTBOX_MESSAGE");

        Delete.Table("OUTBOX_MESSAGE");
        Delete.Table("USER_PASSWORD");
        Delete.Table("USER_EMAIL");
        Delete.Table("USER");
    }

    private void CreateUsers()
    {
        Create.Table("USER")
            .WithBaseColumns("PK_USER")
            .WithColumn("IS_ACTIVE").AsBoolean().NotNullable();
    }

    private void CreateUserEmails()
    {
        Create.Table("USER_EMAIL")
            .WithBaseColumns("PK_USER_EMAIL")
            .WithColumn("EMAIL").AsCustom("text").NotNullable()
            .WithColumn("IS_VERIFIED").AsBoolean().NotNullable()
            .WithColumn("START_AT").AsDateTime().NotNullable()
            .WithColumn("USER_ID").AsGuid().NotNullable();
    }

    private void CreateUserPasswords()
    {
        Create.Table("USER_PASSWORD")
            .WithBaseColumns("PK_USER_PASSWORD")
            .WithColumn("PASSWORD_HASH").AsCustom("text").NotNullable()
            .WithColumn("START_AT").AsDateTime().NotNullable()
            .WithColumn("USER_ID").AsGuid().NotNullable();
    }

    private void CreateOutboxMessages()
    {
        Create.Table("OUTBOX_MESSAGE")
            .WithBaseColumns("PK_OUTBOX_MESSAGE")
            .WithColumn("TYPE").AsCustom("text").NotNullable()
            .WithColumn("PAYLOAD").AsCustom("text").NotNullable()
            .WithColumn("OCCURRED_AT").AsDateTime().NotNullable()
            .WithColumn("PROCESSED_AT").AsDateTime().Nullable()
            .WithColumn("ERROR").AsCustom("text").Nullable()
            .WithColumn("RETRY_COUNT").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("MAX_RETRY_COUNT").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("NEXT_RETRY_AT").AsDateTime().Nullable();
    }

    private void CreateIndexes()
    {
        Create.Index("IX_USER_EMAIL_EMAIL")
            .OnTable("USER_EMAIL")
            .OnColumn("EMAIL")
            .Ascending()
            .WithOptions()
            .Unique();

        Create.Index("IX_USER_EMAIL_USER_ID")
            .OnTable("USER_EMAIL")
            .OnColumn("USER_ID");

        Create.Index("IX_USER_PASSWORD_USER_ID")
            .OnTable("USER_PASSWORD")
            .OnColumn("USER_ID");

        Create.Index("IX_OUTBOX_MESSAGE_PROCESSED_AT")
            .OnTable("OUTBOX_MESSAGE")
            .OnColumn("PROCESSED_AT");

        Create.Index("IX_OUTBOX_MESSAGE_NEXT_RETRY_AT")
            .OnTable("OUTBOX_MESSAGE")
            .OnColumn("NEXT_RETRY_AT");
    }

    private void CreateForeignKeys()
    {
        Create.ForeignKey("FK_USER_EMAIL_USER_USER_ID")
            .FromTable("USER_EMAIL").ForeignColumn("USER_ID")
            .ToTable("USER").PrimaryColumn("ID")
            .OnDelete(Rule.Cascade);

        Create.ForeignKey("FK_USER_PASSWORD_USER_USER_ID")
            .FromTable("USER_PASSWORD").ForeignColumn("USER_ID")
            .ToTable("USER").PrimaryColumn("ID")
            .OnDelete(Rule.Cascade);
    }
}
