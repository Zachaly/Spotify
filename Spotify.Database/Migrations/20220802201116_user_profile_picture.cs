using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify.Database.Migrations
{
    public partial class user_profile_picture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "AspNetUsers");
        }
    }
}
