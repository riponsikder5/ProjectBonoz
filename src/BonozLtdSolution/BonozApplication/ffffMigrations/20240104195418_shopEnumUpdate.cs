using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonozApplication.Migrations
{
    /// <inheritdoc />
    public partial class shopEnumUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprovesd",
                table: "Shops");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Shops",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Shops");

            migrationBuilder.AddColumn<bool>(
                name: "IsApprovesd",
                table: "Shops",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
