using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify.Database.Migrations
{
    public partial class songmp3file : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumLikes_Albums_AlbumId",
                table: "AlbumLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumLikes_AspNetUsers_ApplicationUserId",
                table: "AlbumLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianFollows_AspNetUsers_ApplicationUserId",
                table: "MusicianFollows");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianFollows_Musicians_MusicianId",
                table: "MusicianFollows");

            migrationBuilder.DropForeignKey(
                name: "FK_SongLikes_AspNetUsers_ApplicationUserId",
                table: "SongLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_SongLikes_Songs_SongId",
                table: "SongLikes");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumLikes_Albums_AlbumId",
                table: "AlbumLikes",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumLikes_AspNetUsers_ApplicationUserId",
                table: "AlbumLikes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianFollows_AspNetUsers_ApplicationUserId",
                table: "MusicianFollows",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianFollows_Musicians_MusicianId",
                table: "MusicianFollows",
                column: "MusicianId",
                principalTable: "Musicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongLikes_AspNetUsers_ApplicationUserId",
                table: "SongLikes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongLikes_Songs_SongId",
                table: "SongLikes",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumLikes_Albums_AlbumId",
                table: "AlbumLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumLikes_AspNetUsers_ApplicationUserId",
                table: "AlbumLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianFollows_AspNetUsers_ApplicationUserId",
                table: "MusicianFollows");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianFollows_Musicians_MusicianId",
                table: "MusicianFollows");

            migrationBuilder.DropForeignKey(
                name: "FK_SongLikes_AspNetUsers_ApplicationUserId",
                table: "SongLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_SongLikes_Songs_SongId",
                table: "SongLikes");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Songs");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumLikes_Albums_AlbumId",
                table: "AlbumLikes",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumLikes_AspNetUsers_ApplicationUserId",
                table: "AlbumLikes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianFollows_AspNetUsers_ApplicationUserId",
                table: "MusicianFollows",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianFollows_Musicians_MusicianId",
                table: "MusicianFollows",
                column: "MusicianId",
                principalTable: "Musicians",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SongLikes_AspNetUsers_ApplicationUserId",
                table: "SongLikes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SongLikes_Songs_SongId",
                table: "SongLikes",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id");
        }
    }
}
