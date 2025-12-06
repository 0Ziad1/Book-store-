using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "age",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "users");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "age",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
