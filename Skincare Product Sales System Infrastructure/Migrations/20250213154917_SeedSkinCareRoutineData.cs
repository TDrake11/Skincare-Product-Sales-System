using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Skincare_Product_Sales_System_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedSkinCareRoutineData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SkinCareRoutines",
                columns: new[] { "Id", "Description", "RoutineName", "SkinTypeId", "Status", "TotalSteps" },
                values: new object[,]
                {
                    { 1, "Giúp kiểm soát dầu, ngăn ngừa mụn và giữ ẩm nhẹ nhàng.", "Lộ trình cho da dầu", 1, 0, 6 },
                    { 2, "Dưỡng ẩm sâu, bảo vệ da khỏi bong tróc và mất nước.", "Lộ trình cho da khô", 2, 0, 6 },
                    { 3, "Cân bằng dầu vùng chữ T và giữ ẩm vùng khô.", "Lộ trình cho da hỗn hợp", 3, 0, 6 },
                    { 4, "Duy trì độ ẩm và bảo vệ da trước tác nhân môi trường.", "Lộ trình cho da thường", 4, 0, 6 },
                    { 5, "Làm dịu da, giảm kích ứng và tăng cường hàng rào bảo vệ.", "Lộ trình cho da nhạy cảm", 5, 0, 6 }
                });

            migrationBuilder.InsertData(
                table: "StepRoutines",
                columns: new[] { "Id", "RoutineId", "Status", "StepDescription", "StepNumber" },
                values: new object[,]
                {
                    { 1, 1, 0, "Tẩy trang để loại bỏ dầu và bụi bẩn", 1 },
                    { 2, 1, 0, "Rửa mặt với sữa rửa mặt kiềm dầu", 2 },
                    { 3, 1, 0, "Dùng toner giúp kiểm soát dầu", 3 },
                    { 4, 1, 0, "Sử dụng serum giảm dầu, ngừa mụn", 4 },
                    { 5, 1, 0, "Dưỡng ẩm nhẹ, không gây bít tắc", 5 },
                    { 6, 1, 0, "Thoa kem chống nắng kiềm dầu", 6 },
                    { 7, 2, 0, "Dùng dầu tẩy trang để cấp ẩm", 1 },
                    { 8, 2, 0, "Rửa mặt với sữa rửa mặt dịu nhẹ", 2 },
                    { 9, 2, 0, "Dùng toner cấp ẩm", 3 },
                    { 10, 2, 0, "Sử dụng serum cấp nước", 4 },
                    { 11, 2, 0, "Dưỡng ẩm chuyên sâu", 5 },
                    { 12, 2, 0, "Thoa kem chống nắng dưỡng ẩm", 6 },
                    { 13, 3, 0, "Tẩy trang để loại bỏ dầu thừa và bụi bẩn", 1 },
                    { 14, 3, 0, "Rửa mặt với sữa rửa mặt dịu nhẹ cân bằng", 2 },
                    { 15, 3, 0, "Dùng toner giúp cân bằng da", 3 },
                    { 16, 3, 0, "Sử dụng serum dưỡng ẩm nhẹ", 4 },
                    { 17, 3, 0, "Dưỡng ẩm dạng gel hoặc lotion", 5 },
                    { 18, 3, 0, "Thoa kem chống nắng có độ ẩm vừa phải", 6 },
                    { 19, 4, 0, "Tẩy trang với dầu hoặc nước micellar", 1 },
                    { 20, 4, 0, "Rửa mặt với sữa rửa mặt nhẹ nhàng", 2 },
                    { 21, 4, 0, "Dùng toner cấp ẩm hoặc làm dịu da", 3 },
                    { 22, 4, 0, "Sử dụng serum tăng cường bảo vệ da", 4 },
                    { 23, 4, 0, "Dưỡng ẩm dạng kem hoặc gel phù hợp", 5 },
                    { 24, 4, 0, "Thoa kem chống nắng SPF 30+", 6 },
                    { 25, 5, 0, "Tẩy trang dịu nhẹ, tránh sản phẩm có cồn", 1 },
                    { 26, 5, 0, "Rửa mặt với sữa rửa mặt không chứa hương liệu", 2 },
                    { 27, 5, 0, "Dùng toner không chứa cồn", 3 },
                    { 28, 5, 0, "Sử dụng serum phục hồi da", 4 },
                    { 29, 5, 0, "Dưỡng ẩm chuyên biệt cho da nhạy cảm", 5 },
                    { 30, 5, 0, "Thoa kem chống nắng vật lý dịu nhẹ", 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "StepRoutines",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "SkinCareRoutines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SkinCareRoutines",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SkinCareRoutines",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SkinCareRoutines",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SkinCareRoutines",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
