using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationAgenda.Migrations
{
    public partial class pepe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserId1",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_UserId1",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "IsBlocked" },
                values: new object[] { "Plomero", true });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Papa");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "IsBlocked" },
                values: new object[] { "Jefa", true });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CelularNumber", "Description", "IsBlocked", "Name", "TelephoneNumber", "UserId" },
                values: new object[] { 4, 34156, "?", true, "Juanfer", 42256, 3 });

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserId",
                table: "Contacts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserId",
                table: "Contacts");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Contacts",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserId1",
                table: "Contacts",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserId",
                table: "Contacts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserId1",
                table: "Contacts",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
