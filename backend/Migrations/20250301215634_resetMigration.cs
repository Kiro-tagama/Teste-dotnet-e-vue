using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class resetMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Electronic devices", "Electronics", new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Various books", "Books", new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Men and Women Clothing", "Clothing", new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Name", "Price", "Quantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Android phone", "Smartphone", 699.99m, 10, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Gaming laptop", "Laptop", 1200.50m, 5, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("aaaaaaa3-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Fitness watch", "Smartwatch", 199.99m, 20, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Learn C#", "C# Programming", 39.99m, 15, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Web development", "ASP.NET Core", 49.99m, 10, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("bbbbbbb3-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "SQL and NoSQL databases", "Database Design", 29.99m, 12, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ccccccc1-cccc-cccc-cccc-cccccccccccc"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Cotton T-shirt", "T-shirt", 19.99m, 50, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ccccccc2-cccc-cccc-cccc-cccccccccccc"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Blue denim jeans", "Jeans", 49.99m, 25, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ccccccc3-cccc-cccc-cccc-cccccccccccc"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "Winter jacket", "Jacket", 89.99m, 8, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
