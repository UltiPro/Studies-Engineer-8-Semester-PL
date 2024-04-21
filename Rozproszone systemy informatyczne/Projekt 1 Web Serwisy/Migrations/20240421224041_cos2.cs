using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt_1_Web_Serwisy.Migrations
{
    /// <inheritdoc />
    public partial class cos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Reservation",
                table: "Motors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 1,
                column: "Reservation",
                value: false);

            migrationBuilder.UpdateData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 2,
                column: "Reservation",
                value: false);

            migrationBuilder.UpdateData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 3,
                column: "Reservation",
                value: false);

            migrationBuilder.UpdateData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 4,
                column: "Reservation",
                value: false);

            migrationBuilder.UpdateData(
                table: "Motors",
                keyColumn: "Id",
                keyValue: 5,
                column: "Reservation",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reservation",
                table: "Motors");
        }
    }
}
