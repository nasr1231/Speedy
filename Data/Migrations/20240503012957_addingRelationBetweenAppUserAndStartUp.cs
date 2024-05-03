using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedy.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingRelationBetweenAppUserAndStartUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StartUps_AppUserId",
                table: "StartUps");

            migrationBuilder.CreateIndex(
                name: "IX_StartUps_AppUserId",
                table: "StartUps",
                column: "AppUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StartUps_AppUserId",
                table: "StartUps");

            migrationBuilder.CreateIndex(
                name: "IX_StartUps_AppUserId",
                table: "StartUps",
                column: "AppUserId");
        }
    }
}
