using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingForeignKeyFluentApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Cities_CityId1",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_CityId1",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "CityId1",
                table: "Deliveries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Deliveries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_CityId",
                table: "Deliveries",
                column: "CityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Cities_CityId",
                table: "Deliveries",
                column: "CityId1",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}
