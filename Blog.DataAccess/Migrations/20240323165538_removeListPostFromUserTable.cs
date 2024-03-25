using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removeListPostFromUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0cd7180-797e-4da7-8e5b-1592474f6642");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d3c6d22a-745a-4e06-b1b9-0a21f18c8cfd");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "55617164-7da5-49fe-8564-fca53c32b899", null, "User", "USER" },
                    { "864a4c6b-32ba-42a2-aa83-a49d430933e8", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "55617164-7da5-49fe-8564-fca53c32b899");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "864a4c6b-32ba-42a2-aa83-a49d430933e8");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d0cd7180-797e-4da7-8e5b-1592474f6642", null, "Admin", "ADMIN" },
                    { "d3c6d22a-745a-4e06-b1b9-0a21f18c8cfd", null, "User", "USER" }
                });
        }
    }
}
