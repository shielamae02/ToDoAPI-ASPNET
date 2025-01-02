using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoAPI_ASPNET.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTokenEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Expiration",
                table: "Tokens",
                newName: "ExpiresAt");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Tokens",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Tokens");

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                table: "Tokens",
                newName: "Expiration");
        }
    }
}
