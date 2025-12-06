using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UTB.BaChr.Mapy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mysql_100_FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "20bb2632-8d71-450a-9798-bb76403046d4", "admin@admin.cz", true, "Hlavní", "Administrátor", false, null, "ADMIN@ADMIN.CZ", "ADMIN", "AQAAAAIAAYagAAAAEP9nV8XPNjBMGGsJYdVL0m/WnYNwT7zmcN1SlpdFTX84aMm2whc8+lVfmdCpKA4JZg==", null, false, "009352db-0722-42bd-8831-ae3b3a334cb4", false, "admin" },
                    { 2, 0, "bb0133c7-fc9f-4b9d-a720-0d0e3eddf160", "klient@klient.cz", true, "Jan", "Novák", false, null, "KLIENT@KLIENT.CZ", "KLIENT", "AQAAAAIAAYagAAAAEB4aiyNx34eJ+bx4c3fOr+Tt1Poann6LRPsTdz9surXScY3UI9zTm6AY8KiEKFM9GQ==", null, false, "abd3f24f-b744-4b99-9266-ff379cc9941e", false, "klient" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
