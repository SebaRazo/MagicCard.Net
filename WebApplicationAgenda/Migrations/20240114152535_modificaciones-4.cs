using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationAgenda.Migrations
{
    public partial class modificaciones4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: -1,
                column: "TimeCall",
                value: new DateTime(2024, 1, 14, 11, 25, 35, 485, DateTimeKind.Local).AddTicks(6556));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: -1,
                column: "TimeCall",
                value: new DateTime(2024, 1, 14, 11, 2, 9, 411, DateTimeKind.Local).AddTicks(7189));
        }
    }
}
