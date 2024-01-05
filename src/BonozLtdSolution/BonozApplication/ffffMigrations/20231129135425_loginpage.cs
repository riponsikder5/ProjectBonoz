using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BonozApplication.Migrations
{
    /// <inheritdoc />
    public partial class loginpage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoginId",
                table: "Users",
                newName: "UserName");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "LoginId");
        }
    }
}
