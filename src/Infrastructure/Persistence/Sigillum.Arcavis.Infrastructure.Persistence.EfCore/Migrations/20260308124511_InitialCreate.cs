using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OUTBOX_MESSAGE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    CREATED_BY = table.Column<Guid>(type: "uuid", nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UPDATED_BY = table.Column<Guid>(type: "uuid", nullable: true),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IS_DELETED = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TYPE = table.Column<string>(type: "text", nullable: false),
                    PAYLOAD = table.Column<string>(type: "text", nullable: false),
                    OCCURRED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PROCESSED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ERROR = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OUTBOX_MESSAGE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    CREATED_BY = table.Column<Guid>(type: "uuid", nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UPDATED_BY = table.Column<Guid>(type: "uuid", nullable: true),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IS_DELETED = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IS_ACTIVE = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USER_EMAIL",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    EMAIL = table.Column<string>(type: "text", nullable: false),
                    IS_VERIFIED = table.Column<bool>(type: "boolean", nullable: false),
                    USER_ID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_EMAIL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_EMAIL_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_PASSWORD",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PASSWORD_HASH = table.Column<string>(type: "text", nullable: false),
                    USER_ID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_PASSWORD", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_PASSWORD_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_EMAIL_EMAIL",
                table: "USER_EMAIL",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_EMAIL_USER_ID",
                table: "USER_EMAIL",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_PASSWORD_USER_ID",
                table: "USER_PASSWORD",
                column: "USER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OUTBOX_MESSAGE");

            migrationBuilder.DropTable(
                name: "USER_EMAIL");

            migrationBuilder.DropTable(
                name: "USER_PASSWORD");

            migrationBuilder.DropTable(
                name: "USER");
        }
    }
}
