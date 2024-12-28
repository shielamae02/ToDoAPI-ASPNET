using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoAPI_ASPNET.Migrations
{
    /// <inheritdoc />
    public partial class FixNamingConvention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isComplete",
                table: "ToDoItems",
                newName: "IsComplete");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsComplete",
                table: "ToDoItems",
                newName: "isComplete");
        }
    }
}
