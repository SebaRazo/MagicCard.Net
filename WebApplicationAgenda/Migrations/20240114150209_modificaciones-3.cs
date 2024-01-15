using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationAgenda.Migrations
{
    public partial class modificaciones3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: -1,
                column: "TimeCall",
                value: new DateTime(2024, 1, 14, 11, 2, 9, 411, DateTimeKind.Local).AddTicks(7189));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: -1,
                column: "TimeCall",
                value: new DateTime(2024, 1, 14, 11, 1, 44, 486, DateTimeKind.Local).AddTicks(4085));
        }
    }
}
