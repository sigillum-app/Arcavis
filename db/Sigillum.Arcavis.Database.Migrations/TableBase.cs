using FluentMigrator.Builders.Create.Table;

namespace Sigillum.Arcavis.Database.Migrations;

internal static class TableBase
{
    public static ICreateTableColumnOptionOrWithColumnSyntax WithBaseColumns( this ICreateTableWithColumnSyntax table, string primaryKey)
    {
        return table
            .WithColumn("ID").AsGuid().PrimaryKey(primaryKey)
            .WithColumn("CREATED_AT").AsDateTime().NotNullable()
            .WithColumn("CREATED_BY").AsGuid().Nullable()
            .WithColumn("UPDATED_AT").AsDateTime().NotNullable()
            .WithColumn("UPDATED_BY").AsGuid().Nullable();
    }
}
