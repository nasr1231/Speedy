using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedy.Data.Migrations
{
    /// <inheritdoc />
    public partial class HoldPaymentMethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_PaymentMethods_PaymentMethodId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Individuals_PaymentMethods_PaymentMethodId",
                table: "Individuals");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentMethodId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Individuals_PaymentMethodId",
                table: "Individuals");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_PaymentMethodId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Individuals");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Deliveries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "Individuals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_PaymentMethodId",
                table: "Individuals",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_PaymentMethodId",
                table: "Deliveries",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_PaymentMethods_PaymentMethodId",
                table: "Deliveries",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Individuals_PaymentMethods_PaymentMethodId",
                table: "Individuals",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentMethods_PaymentMethodId",
                table: "Orders",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id");
        }
    }
}
