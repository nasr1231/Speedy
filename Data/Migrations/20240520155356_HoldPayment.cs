using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedy.Data.Migrations
{
    /// <inheritdoc />
    public partial class HoldPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StartUps_PaymentMethods_PaymentMethodId",
                table: "StartUps");

            migrationBuilder.DropIndex(
                name: "IX_StartUps_PaymentMethodId",
                table: "StartUps");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "StartUps");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "StartUps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StartUps_PaymentMethodId",
                table: "StartUps",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_StartUps_PaymentMethods_PaymentMethodId",
                table: "StartUps",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
