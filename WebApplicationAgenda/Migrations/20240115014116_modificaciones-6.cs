using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationAgenda.Migrations
{
    public partial class modificaciones6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "Calls",
                columns: new[] { "Id", "ContactId", "CountCall", "TimeCall" },
                values: new object[] { 1, 1, 2, new DateTime(2024, 1, 14, 21, 41, 15, 788, DateTimeKind.Local).AddTicks(6730) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Calls",
                columns: new[] { "Id", "ContactId", "CountCall", "TimeCall" },
                values: new object[] { -1, 1, 2, new DateTime(2024, 1, 14, 11, 37, 21, 215, DateTimeKind.Local).AddTicks(2646) });
        }
    }
}
