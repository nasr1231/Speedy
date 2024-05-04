using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingRelationToReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "StartUps",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IndividualId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_IndividualId",
                table: "Reviews",
                column: "IndividualId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Individuals_IndividualId",
                table: "Reviews",
                column: "IndividualId",
                principalTable: "Individuals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Individuals_IndividualId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_IndividualId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IndividualId",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "StartUps",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
