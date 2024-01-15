using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationAgenda.Migrations
{
    public partial class modificaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Calls_ContactId",
                table: "Calls");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsBlocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsBlocked",
                value: false);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsBlocked",
                value: false);

            migrationBuilder.CreateIndex(
                name: "IX_Calls_ContactId",
                table: "Calls",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserId",
                table: "Contacts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Calls_ContactId",
                table: "Calls");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsBlocked",
                value: true);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsBlocked",
                value: true);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsBlocked",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calls_ContactId",
                table: "Calls",
                column: "ContactId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserId",
                table: "Contacts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
