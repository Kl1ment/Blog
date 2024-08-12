using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEntityUserEntity_Users_FolowersId",
                table: "UserEntityUserEntity");

            migrationBuilder.RenameColumn(
                name: "FolowersId",
                table: "UserEntityUserEntity",
                newName: "FollowersId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEntityUserEntity_Users_FollowersId",
                table: "UserEntityUserEntity",
                column: "FollowersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEntityUserEntity_Users_FollowersId",
                table: "UserEntityUserEntity");

            migrationBuilder.RenameColumn(
                name: "FollowersId",
                table: "UserEntityUserEntity",
                newName: "FolowersId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEntityUserEntity_Users_FolowersId",
                table: "UserEntityUserEntity",
                column: "FolowersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
