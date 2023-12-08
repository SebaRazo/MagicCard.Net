using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationAgenda.Migrations
{
    public partial class retoques : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LastName", "Name", "Password", "UserName" },
                values: new object[] { 3, "sr@gmail.com", "Razo", "Seba", "seba", "sr" });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserId1",
                table: "Contacts",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UserId1",
                table: "Contacts",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UserId1",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_UserId1",
                table: "Contacts");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Contacts");
        }
    }
}
