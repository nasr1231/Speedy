using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedy.Data.Migrations
{
    /// <inheritdoc />
    public partial class DropMobileNumberAtrrDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Governorates_GovernorateId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_MobileNumber",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "LegalStatus",
                table: "StartUps");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Deliveries");

            migrationBuilder.AlterColumn<int>(
                name: "GovernorateId",
                table: "Deliveries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Governorates_GovernorateId",
                table: "Deliveries",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Governorates_GovernorateId",
                table: "Deliveries");

            migrationBuilder.AddColumn<string>(
                name: "LegalStatus",
                table: "StartUps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "GovernorateId",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Deliveries",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_MobileNumber",
                table: "Deliveries",
                column: "MobileNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Governorates_GovernorateId",
                table: "Deliveries",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
