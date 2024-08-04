using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashPassword",
                table: "UsersLogin");

            migrationBuilder.AddColumn<string>(
                name: "passwordHash",
                table: "UsersLogin",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "UsersLogin");

            migrationBuilder.AddColumn<int>(
                name: "HashPassword",
                table: "UsersLogin",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
