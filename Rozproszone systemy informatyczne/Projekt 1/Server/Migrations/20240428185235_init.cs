using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projekt_1_Web_Serwisy.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Motors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentPrice = table.Column<int>(type: "int", nullable: false),
                    RentTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reservation = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Motors",
                columns: new[] { "Id", "Name", "RentPrice", "RentTo", "Reservation" },
                values: new object[,]
                {
                    { 1, "a", 1000, null, false },
                    { 2, "b", 1000, null, false },
                    { 3, "c", 1000, null, false },
                    { 4, "d", 1000, null, false },
                    { 5, "e", 1000, null, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Motors");
        }
    }
}
