using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify.Database.Migrations
{
    public partial class manager_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "Musicians",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MusicianId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MusicianId",
                table: "AspNetUsers",
                column: "MusicianId",
                unique: true,
                filter: "[MusicianId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Musicians_MusicianId",
                table: "AspNetUsers",
                column: "MusicianId",
                principalTable: "Musicians",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Musicians_MusicianId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MusicianId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Musicians");

            migrationBuilder.DropColumn(
                name: "MusicianId",
                table: "AspNetUsers");
        }
    }
}
