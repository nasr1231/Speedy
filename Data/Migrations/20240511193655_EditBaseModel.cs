using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedy.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditBaseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedById",
                table: "StartUps",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "StartUps",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedById",
                table: "ShippingMethods",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "ShippingMethods",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedById",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedById",
                table: "Individuals",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Individuals",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedById",
                table: "Governorates",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Governorates",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedById",
                table: "Deliveries",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Deliveries",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedById",
                table: "Cities",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Cities",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ShippingMethods",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ShippingMethods",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ShippingMethods",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_StartUps_CreatedById",
                table: "StartUps",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StartUps_LastUpdatedById",
                table: "StartUps",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingMethods_CreatedById",
                table: "ShippingMethods",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingMethods_LastUpdatedById",
                table: "ShippingMethods",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CreatedById",
                table: "Reviews",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_LastUpdatedById",
                table: "Reviews",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_CreatedById",
                table: "Individuals",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_LastUpdatedById",
                table: "Individuals",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Governorates_CreatedById",
                table: "Governorates",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Governorates_LastUpdatedById",
                table: "Governorates",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_CreatedById",
                table: "Deliveries",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_LastUpdatedById",
                table: "Deliveries",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CreatedById",
                table: "Cities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_LastUpdatedById",
                table: "Cities",
                column: "LastUpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_AspNetUsers_CreatedById",
                table: "Cities",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_AspNetUsers_LastUpdatedById",
                table: "Cities",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_CreatedById",
                table: "Deliveries",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_AspNetUsers_LastUpdatedById",
                table: "Deliveries",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Governorates_AspNetUsers_CreatedById",
                table: "Governorates",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Governorates_AspNetUsers_LastUpdatedById",
                table: "Governorates",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Individuals_AspNetUsers_CreatedById",
                table: "Individuals",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Individuals_AspNetUsers_LastUpdatedById",
                table: "Individuals",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_CreatedById",
                table: "Reviews",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_LastUpdatedById",
                table: "Reviews",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingMethods_AspNetUsers_CreatedById",
                table: "ShippingMethods",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingMethods_AspNetUsers_LastUpdatedById",
                table: "ShippingMethods",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StartUps_AspNetUsers_CreatedById",
                table: "StartUps",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StartUps_AspNetUsers_LastUpdatedById",
                table: "StartUps",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_AspNetUsers_CreatedById",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_AspNetUsers_LastUpdatedById",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_CreatedById",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_AspNetUsers_LastUpdatedById",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Governorates_AspNetUsers_CreatedById",
                table: "Governorates");

            migrationBuilder.DropForeignKey(
                name: "FK_Governorates_AspNetUsers_LastUpdatedById",
                table: "Governorates");

            migrationBuilder.DropForeignKey(
                name: "FK_Individuals_AspNetUsers_CreatedById",
                table: "Individuals");

            migrationBuilder.DropForeignKey(
                name: "FK_Individuals_AspNetUsers_LastUpdatedById",
                table: "Individuals");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_CreatedById",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_LastUpdatedById",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingMethods_AspNetUsers_CreatedById",
                table: "ShippingMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingMethods_AspNetUsers_LastUpdatedById",
                table: "ShippingMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_StartUps_AspNetUsers_CreatedById",
                table: "StartUps");

            migrationBuilder.DropForeignKey(
                name: "FK_StartUps_AspNetUsers_LastUpdatedById",
                table: "StartUps");

            migrationBuilder.DropIndex(
                name: "IX_StartUps_CreatedById",
                table: "StartUps");

            migrationBuilder.DropIndex(
                name: "IX_StartUps_LastUpdatedById",
                table: "StartUps");

            migrationBuilder.DropIndex(
                name: "IX_ShippingMethods_CreatedById",
                table: "ShippingMethods");

            migrationBuilder.DropIndex(
                name: "IX_ShippingMethods_LastUpdatedById",
                table: "ShippingMethods");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CreatedById",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_LastUpdatedById",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Individuals_CreatedById",
                table: "Individuals");

            migrationBuilder.DropIndex(
                name: "IX_Individuals_LastUpdatedById",
                table: "Individuals");

            migrationBuilder.DropIndex(
                name: "IX_Governorates_CreatedById",
                table: "Governorates");

            migrationBuilder.DropIndex(
                name: "IX_Governorates_LastUpdatedById",
                table: "Governorates");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_CreatedById",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_LastUpdatedById",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CreatedById",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_LastUpdatedById",
                table: "Cities");

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedById",
                table: "StartUps",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "StartUps",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedById",
                table: "ShippingMethods",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "ShippingMethods",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedById",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedById",
                table: "Individuals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Individuals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedById",
                table: "Governorates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Governorates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedById",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedById",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ShippingMethods",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ShippingMethods",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ShippingMethods",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
