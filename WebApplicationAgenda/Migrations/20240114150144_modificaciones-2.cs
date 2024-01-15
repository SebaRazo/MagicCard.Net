using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationAgenda.Migrations
{
    public partial class modificaciones2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Calls",
                columns: new[] { "Id", "ContactId", "CountCall", "TimeCall" },
                values: new object[] { -1, 1, 2, new DateTime(2024, 1, 14, 11, 1, 44, 486, DateTimeKind.Local).AddTicks(4085) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Calls",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
