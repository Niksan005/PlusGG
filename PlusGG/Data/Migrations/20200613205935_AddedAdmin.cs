using Microsoft.EntityFrameworkCore.Migrations;

namespace PlusGG.Data.Migrations
{
    public partial class AddedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1b821ed5-85f0-4dab-85d8-626596989a2b", "296114d0-45a8-40ed-8412-85a6df36a2c9", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "444955db-cdf3-4934-91b0-fc6ce4f53154", 0, "16d0480f-91e8-4038-9534-ccf336a22ac0", "Admin@admin", true, false, null, "admin@admin", "admin@admin", "AQAAAAEAACcQAAAAEPaF8/kbgEeRQYeTehnaudayN5AcmXvYGcPDXOE296Mjx048KYW8UahRMJ8D0bDddg==", null, false, "8ae0b182-5033-4881-8b09-5b589a8fd56d", false, "Admin@admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "444955db-cdf3-4934-91b0-fc6ce4f53154", "1b821ed5-85f0-4dab-85d8-626596989a2b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "444955db-cdf3-4934-91b0-fc6ce4f53154", "1b821ed5-85f0-4dab-85d8-626596989a2b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b821ed5-85f0-4dab-85d8-626596989a2b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "444955db-cdf3-4934-91b0-fc6ce4f53154");
        }
    }
}
