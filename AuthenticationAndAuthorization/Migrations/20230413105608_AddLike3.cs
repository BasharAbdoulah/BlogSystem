using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddLike3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_likes_PostId",
                table: "likes",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_likes_Posts_PostId",
                table: "likes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_likes_Posts_PostId",
                table: "likes");

            migrationBuilder.DropIndex(
                name: "IX_likes_PostId",
                table: "likes");
        }
    }
}
