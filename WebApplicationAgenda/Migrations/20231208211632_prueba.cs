using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationAgenda.Migrations
{
    public partial class prueba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserId1",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Contacts",
                newName: "BlockedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_UserId1",
                table: "Contacts",
                newName: "IX_Contacts_BlockedByUserId");

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

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "BlockedByUserId", "CelularNumber", "IsBlocked", "Name", "TelephoneNumber", "UserId" },
                values: new object[] { 4, null, 34156, true, "Juanfer", 42256, 3 });

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_BlockedByUserId",
                table: "Contacts",
                column: "BlockedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_BlockedByUserId",
                table: "Contacts");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "BlockedByUserId",
                table: "Contacts",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Contacts_BlockedByUserId",
                table: "Contacts",
                newName: "IX_Contacts_UserId1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserId1",
                table: "Contacts",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
