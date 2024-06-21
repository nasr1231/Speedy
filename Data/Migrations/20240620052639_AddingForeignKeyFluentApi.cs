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
