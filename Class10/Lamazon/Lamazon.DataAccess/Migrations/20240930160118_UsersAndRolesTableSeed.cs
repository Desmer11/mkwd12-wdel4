using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lamazon.DataAccess.Migrations
{
    public partial class UsersAndRolesTableSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Key", "Name" },
                values: new object[] { "admin", "Administrator" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Key", "Name" },
                values: new object[] { "user", "User" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "PasswordHash", "RoleKey" },
                values: new object[] { 1, "admin@outlook.com", "Admin User", "AQAAAAEAACcQAAAAECJCSH7Y7+DSAD+UKEnb6fjgOROzppnUpop5/kVMcBDjzOVaLz0vts978iw4ooBhhQ==", "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "PasswordHash", "RoleKey" },
                values: new object[] { 2, "user@outlook.com", "Normal User", "AQAAAAEAACcQAAAAEH2PV/R1HciXgHqwrYcEp/32IrxaQ44wcbBnM6EHK2FXA5wZRYXN6pddtVKNqTpTxg==", "user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Key",
                keyValue: "admin");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Key",
                keyValue: "user");
        }
    }
}
