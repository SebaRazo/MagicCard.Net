using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationAgenda.Migrations
{
    public partial class modificaciones8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Calls",
                columns: new[] { "Id", "ContactId", "CountCall", "TimeCall" },
                values: new object[] { 2, 2, 2, new DateTime(2024, 1, 14, 21, 50, 24, 843, DateTimeKind.Local).AddTicks(3455) });
        }
    }
}
