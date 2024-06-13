using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StartUpId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_StartUpId",
                table: "Reviews",
                column: "StartUpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_StartUps_StartUpId",
                table: "Reviews",
                column: "StartUpId",
                principalTable: "StartUps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_StartUps_StartUpId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_StartUpId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "StartUpId",
                table: "Reviews");
        }
    }
}
