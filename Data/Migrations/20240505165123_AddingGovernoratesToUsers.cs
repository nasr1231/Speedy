using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingGovernoratesToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GovernorateId",
                table: "StartUps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GovernorateId",
                table: "Individuals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StartUps_GovernorateId",
                table: "StartUps",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_GovernorateId",
                table: "Individuals",
                column: "GovernorateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Individuals_Governorates_GovernorateId",
                table: "Individuals",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StartUps_Governorates_GovernorateId",
                table: "StartUps",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Individuals_Governorates_GovernorateId",
                table: "Individuals");

            migrationBuilder.DropForeignKey(
                name: "FK_StartUps_Governorates_GovernorateId",
                table: "StartUps");

            migrationBuilder.DropIndex(
                name: "IX_StartUps_GovernorateId",
                table: "StartUps");

            migrationBuilder.DropIndex(
                name: "IX_Individuals_GovernorateId",
                table: "Individuals");

            migrationBuilder.DropColumn(
                name: "GovernorateId",
                table: "StartUps");

            migrationBuilder.DropColumn(
                name: "GovernorateId",
                table: "Individuals");
        }
    }
}
