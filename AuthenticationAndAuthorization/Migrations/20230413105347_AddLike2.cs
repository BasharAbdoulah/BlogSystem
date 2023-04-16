using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddLike2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_likes_UserId",
                table: "likes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_likes_Users_UserId",
                table: "likes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_likes_Users_UserId",
                table: "likes");

            migrationBuilder.DropIndex(
                name: "IX_likes_UserId",
                table: "likes");
        }
    }
}
