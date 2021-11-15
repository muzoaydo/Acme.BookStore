using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.BookStore.Migrations
{
    public partial class Added_Authors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppAuthors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShortBio = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAuthors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppAuthors_Name",
                table: "AppAuthors",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppAuthors");
        }
    }
}
