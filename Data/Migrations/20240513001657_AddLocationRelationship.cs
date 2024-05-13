using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "StartUps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Individuals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StartUps_CityId",
                table: "StartUps",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_CityId",
                table: "Individuals",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Individuals_Cities_CityId",
                table: "Individuals",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StartUps_Cities_CityId",
                table: "StartUps",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Individuals_Cities_CityId",
                table: "Individuals");

            migrationBuilder.DropForeignKey(
                name: "FK_StartUps_Cities_CityId",
                table: "StartUps");

            migrationBuilder.DropIndex(
                name: "IX_StartUps_CityId",
                table: "StartUps");

            migrationBuilder.DropIndex(
                name: "IX_Individuals_CityId",
                table: "Individuals");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "StartUps");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Individuals");
        }
    }
}
