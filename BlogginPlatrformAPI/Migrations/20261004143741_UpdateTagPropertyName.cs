using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogginPlatformAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTagPropertyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tags",
                newName: "Tags");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tags",
                table: "Tags",
                newName: "Name");
        }
    }
}
