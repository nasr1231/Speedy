using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedy.Data.Migrations
{
    /// <inheritdoc />
    public partial class DropGovernorateIdForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Governorates_GovernorateId",
                table: "Deliveries");

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

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_GovernorateId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "GovernorateId",
                table: "StartUps");

            migrationBuilder.DropColumn(
                name: "GovernorateId",
                table: "Individuals");

            migrationBuilder.DropColumn(
                name: "GovernorateId",
                table: "Deliveries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "GovernorateId",
                table: "Deliveries",
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

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_GovernorateId",
                table: "Deliveries",
                column: "GovernorateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Governorates_GovernorateId",
                table: "Deliveries",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "Id");

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
    }
}
