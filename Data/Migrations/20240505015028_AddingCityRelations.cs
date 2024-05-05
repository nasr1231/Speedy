using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingCityRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ShippingMethods");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GovernorateId",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_CityId",
                table: "Deliveries",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_GovernorateId",
                table: "Deliveries",
                column: "GovernorateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Cities_CityId",
                table: "Deliveries",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Governorates_GovernorateId",
                table: "Deliveries",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Cities_CityId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Governorates_GovernorateId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_CityId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_GovernorateId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "GovernorateId",
                table: "Deliveries");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ShippingMethods",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
