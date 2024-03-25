using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addPostTagToPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "55617164-7da5-49fe-8564-fca53c32b899");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "864a4c6b-32ba-42a2-aa83-a49d430933e8");

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_tags",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_tags", x => new { x.PostId, x.TagId });
                    table.ForeignKey(
                        name: "FK_post_tags_posts_PostId",
                        column: x => x.PostId,
                        principalTable: "posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_tags_tags_TagId",
                        column: x => x.TagId,
                        principalTable: "tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0aef5f13-e373-481a-a344-3bbef335c7ab", null, "Admin", "ADMIN" },
                    { "5161278a-a50e-4a4e-846c-0999291a866d", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Lập trình" },
                    { 2, "Công nghệ" },
                    { 3, "OOP" },
                    { 4, "Quản lý dự án" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_post_tags_TagId",
                table: "post_tags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "post_tags");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0aef5f13-e373-481a-a344-3bbef335c7ab");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5161278a-a50e-4a4e-846c-0999291a866d");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "55617164-7da5-49fe-8564-fca53c32b899", null, "User", "USER" },
                    { "864a4c6b-32ba-42a2-aa83-a49d430933e8", null, "Admin", "ADMIN" }
                });
        }
    }
}
