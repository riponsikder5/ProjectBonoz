using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonozApplication.Migrations
{
    /// <inheritdoc />
    public partial class hfjsgdsjhfkg78 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Categories",
                newName: "IconCSS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IconCSS",
                table: "Categories",
                newName: "Description");
        }
    }
}
