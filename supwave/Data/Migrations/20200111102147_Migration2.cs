using Microsoft.EntityFrameworkCore.Migrations;

namespace supwave.Data.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_AspNetUsers_UserId",
                table: "Playlist");

            migrationBuilder.DropIndex(
                name: "IX_Playlist_UserId",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Playlist");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Playlist",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "Playlist");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Playlist",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_UserId",
                table: "Playlist",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_AspNetUsers_UserId",
                table: "Playlist",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
