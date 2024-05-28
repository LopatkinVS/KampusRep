using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kampus.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixingDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "User",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Reviews",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Professors",
                newName: "Name");

            migrationBuilder.AlterColumn<float>(
                name: "Raiting",
                table: "Professors",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Reviews",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Professors",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Raiting",
                table: "Professors",
                type: "integer",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
