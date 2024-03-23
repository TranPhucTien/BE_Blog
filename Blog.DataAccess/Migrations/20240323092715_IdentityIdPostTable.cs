using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class IdentityIdPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 3, 23, 16, 27, 14, 634, DateTimeKind.Local).AddTicks(9760), new DateTime(2024, 3, 23, 16, 27, 14, 634, DateTimeKind.Local).AddTicks(9778), new DateTime(2024, 3, 23, 16, 27, 14, 634, DateTimeKind.Local).AddTicks(9777) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "posts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 3, 23, 12, 44, 18, 498, DateTimeKind.Local).AddTicks(2201), new DateTime(2024, 3, 23, 12, 44, 18, 498, DateTimeKind.Local).AddTicks(2213), new DateTime(2024, 3, 23, 12, 44, 18, 498, DateTimeKind.Local).AddTicks(2212) });
        }
    }
}
