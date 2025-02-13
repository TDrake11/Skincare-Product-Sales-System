using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Skincare_Product_Sales_System_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedSkinTypeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SkinTypes",
                columns: new[] { "Id", "SkinTypeName" },
                values: new object[,]
                {
                    { 1, "Da dầu" },
                    { 2, "Da khô" },
                    { 3, "Da hỗn hợp" },
                    { 4, "Da thường" },
                    { 5, "Da nhạy cảm" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SkinTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SkinTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SkinTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SkinTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SkinTypes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
