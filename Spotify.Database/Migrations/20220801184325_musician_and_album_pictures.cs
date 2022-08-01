using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify.Database.Migrations
{
    public partial class musician_and_album_pictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Musicians",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Musicians");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Albums");
        }
    }
}
