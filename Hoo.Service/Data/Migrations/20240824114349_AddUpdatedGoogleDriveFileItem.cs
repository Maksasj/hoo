using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hoo.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedGoogleDriveFileItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleId",
                table: "GoogleDriveFiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleId",
                table: "GoogleDriveFiles");
        }
    }
}
