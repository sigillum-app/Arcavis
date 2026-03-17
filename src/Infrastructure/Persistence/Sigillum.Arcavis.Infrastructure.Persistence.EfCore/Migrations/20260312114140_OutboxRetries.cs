using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class OutboxRetries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MAX_RETRY_COUNT",
                table: "OUTBOX_MESSAGE",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 12);

            migrationBuilder.AddColumn<DateTime>(
                name: "NEXT_RETRY_AT",
                table: "OUTBOX_MESSAGE",
                type: "timestamp with time zone",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 13);

            migrationBuilder.AddColumn<int>(
                name: "RETRY_COUNT",
                table: "OUTBOX_MESSAGE",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 11);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MAX_RETRY_COUNT",
                table: "OUTBOX_MESSAGE");

            migrationBuilder.DropColumn(
                name: "NEXT_RETRY_AT",
                table: "OUTBOX_MESSAGE");

            migrationBuilder.DropColumn(
                name: "RETRY_COUNT",
                table: "OUTBOX_MESSAGE");
        }
    }
}
