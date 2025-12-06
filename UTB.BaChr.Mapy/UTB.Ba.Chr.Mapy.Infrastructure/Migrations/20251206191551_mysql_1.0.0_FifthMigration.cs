using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTB.BaChr.Mapy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mysql_100_FifthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Jezero");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7ac61644-d969-4aca-963e-eecbbbac34aa", "AQAAAAIAAYagAAAAEIZBhz8ZL+nW3qRKWR8i8yzH9yqsB7mITnnzlnnqxsbh+F6FvXzH8tLhSRdSyIJQ3Q==", "d2f900f7-9008-4c35-b658-3964749de711" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b5f09198-134e-4f87-88d2-e612ff29cd28", "AQAAAAIAAYagAAAAEC3kMyIDqJG4AhHEVQYvPPv9mK4FVRquJ3vzui6ZnvBuh4jlw6j1ZEnXWpHdikNdNQ==", "f7ca5b07-fe95-4889-ad85-d8c0af4d6998" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Tajemství");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20bb2632-8d71-450a-9798-bb76403046d4", "AQAAAAIAAYagAAAAEP9nV8XPNjBMGGsJYdVL0m/WnYNwT7zmcN1SlpdFTX84aMm2whc8+lVfmdCpKA4JZg==", "009352db-0722-42bd-8831-ae3b3a334cb4" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb0133c7-fc9f-4b9d-a720-0d0e3eddf160", "AQAAAAIAAYagAAAAEB4aiyNx34eJ+bx4c3fOr+Tt1Poann6LRPsTdz9surXScY3UI9zTm6AY8KiEKFM9GQ==", "abd3f24f-b744-4b99-9266-ff379cc9941e" });
        }
    }
}
