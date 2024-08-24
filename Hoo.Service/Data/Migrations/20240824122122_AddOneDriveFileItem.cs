using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hoo.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddOneDriveFileItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GoogleId",
                table: "OneDriveFiles",
                newName: "OneDriveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OneDriveId",
                table: "OneDriveFiles",
                newName: "GoogleId");
        }
    }
}
