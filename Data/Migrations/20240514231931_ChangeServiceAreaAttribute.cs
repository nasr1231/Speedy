using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speedy.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeServiceAreaAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "GovernorateId", "IsDeleted", "LastUpdatedById", "LastUpdatedOn", "Name" },
                values: new object[] { 5, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, null, "وسط البلد" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
