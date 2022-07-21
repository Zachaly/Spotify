using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify.Database.Migrations
{
    public partial class followers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicians_AspNetUsers_ApplicationUserId",
                table: "Musicians");

            migrationBuilder.DropIndex(
                name: "IX_Musicians_ApplicationUserId",
                table: "Musicians");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Musicians");

            migrationBuilder.DropColumn(
                name: "Follows",
                table: "Musicians");

            migrationBuilder.CreateTable(
                name: "ApplicationUserMusician",
                columns: table => new
                {
                    FollowedMusiciansId = table.Column<int>(type: "int", nullable: false),
                    FollowersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserMusician", x => new { x.FollowedMusiciansId, x.FollowersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserMusician_AspNetUsers_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserMusician_Musicians_FollowedMusiciansId",
                        column: x => x.FollowedMusiciansId,
                        principalTable: "Musicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserMusician_FollowersId",
                table: "ApplicationUserMusician",
                column: "FollowersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserMusician");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Musicians",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Follows",
                table: "Musicians",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Musicians_ApplicationUserId",
                table: "Musicians",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musicians_AspNetUsers_ApplicationUserId",
                table: "Musicians",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
