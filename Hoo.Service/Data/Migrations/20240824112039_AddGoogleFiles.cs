using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hoo.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddGoogleFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "WebFiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WebFiles",
                table: "WebFiles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GoogleDriveFiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoogleDriveFiles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoogleDriveFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WebFiles",
                table: "WebFiles");

            migrationBuilder.RenameTable(
                name: "WebFiles",
                newName: "Files");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");
        }
    }
}
