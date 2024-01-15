using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationAgenda.Migrations
{
    public partial class modificaciones5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: -1,
                column: "TimeCall",
                value: new DateTime(2024, 1, 14, 11, 37, 21, 215, DateTimeKind.Local).AddTicks(2646));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: -1,
                column: "TimeCall",
                value: new DateTime(2024, 1, 14, 11, 25, 35, 485, DateTimeKind.Local).AddTicks(6556));
        }
    }
}
