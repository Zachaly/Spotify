using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify.Database.Migrations
{
    public partial class manager_user_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Musicians_MusicianId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MusicianId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MusicianId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "Musicians",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musicians_ManagerId",
                table: "Musicians",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musicians_AspNetUsers_ManagerId",
                table: "Musicians",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicians_AspNetUsers_ManagerId",
                table: "Musicians");

            migrationBuilder.DropIndex(
                name: "IX_Musicians_ManagerId",
                table: "Musicians");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "Musicians",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
    }
}
