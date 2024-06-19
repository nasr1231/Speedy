using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedy.Data.Migrations
{
    /// <inheritdoc />
    public partial class PaymentMethodRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "PaymentMethods",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1,
                column: "AppUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2,
                column: "AppUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 3,
                column: "AppUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 4,
                column: "AppUserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_AppUserId",
                table: "PaymentMethods",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_AspNetUsers_AppUserId",
                table: "PaymentMethods",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_AspNetUsers_AppUserId",
                table: "PaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethods_AppUserId",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "PaymentMethods");
        }
    }
}
