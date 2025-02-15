using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Skincare_Product_Sales_System_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedSkinQuestionData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SkinTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.InsertData(
                table: "SkinQuestions",
                columns: new[] { "Id", "QuestionText" },
                values: new object[,]
                {
                    { 1, "Khi thức dậy vào buổi sáng, bạn cảm thấy da mình thế nào?" },
                    { 2, "Bạn có thường xuyên cảm thấy da mình bóng nhờn vào giữa ngày không?" },
                    { 3, "Da bạn có nhiều dầu vào cuối ngày không?" },
                    { 4, "Khi rửa mặt xong, bạn cảm thấy da mình thế nào?" },
                    { 5, "Da bạn có hay bị bong tróc, ngứa vào mùa lạnh không?" },
                    { 6, "Khi trời nóng, da bạn có đổ dầu nhiều hơn bình thường không?" },
                    { 7, "Bạn có hay bị mụn không?" }
                });

            migrationBuilder.UpdateData(
                table: "SkinTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "SkinTypeName",
                value: "Da hỗn hợp");

            migrationBuilder.UpdateData(
                table: "SkinTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "SkinTypeName",
                value: "Da khô");

            migrationBuilder.InsertData(
                table: "SkinAnswers",
                columns: new[] { "Id", "AnswerText", "QuestionId", "SkinTypeId" },
                values: new object[,]
                {
                    { 1, "Đổ dầu nhiều, bóng nhờn", 1, 1 },
                    { 2, "Chữ T hơi dầu, má bình thường", 1, 2 },
                    { 3, "Khô căng, có thể bong tróc", 1, 3 },
                    { 4, "Ổn định, không dầu cũng không khô", 1, 4 },
                    { 5, "Rất bóng, đặc biệt ở vùng trán và mũi", 2, 1 },
                    { 6, "Chỉ bóng ở vùng chữ T, còn lại bình thường", 2, 2 },
                    { 7, "Không bóng chút nào, có khi còn bong tróc", 2, 3 },
                    { 8, "Hầu như không bóng, vẫn giữ độ ẩm tốt", 2, 4 },
                    { 9, "Rất nhiều dầu, đặc biệt là vùng chữ T", 3, 1 },
                    { 10, "Chỉ vùng chữ T có dầu, hai bên má thì khô", 3, 2 },
                    { 11, "Không có dầu, thường khô căng", 3, 3 },
                    { 12, "Ít dầu, da luôn cân bằng", 3, 4 },
                    { 13, "Nhanh chóng đổ dầu trở lại", 4, 1 },
                    { 14, "Một số vùng đổ dầu, một số vùng khô", 4, 2 },
                    { 15, "Căng rát, thậm chí bong tróc", 4, 3 },
                    { 16, "Mềm mại, thoải mái", 4, 4 },
                    { 17, "Không bao giờ", 5, 1 },
                    { 18, "Thỉnh thoảng, ở hai bên má", 5, 2 },
                    { 19, "KRất hay bong tróc, nhất là quanh mũi và miệng", 5, 3 },
                    { 20, "Hiếm khi bong tróc", 5, 4 },
                    { 21, "Đổ dầu rất nhiều, da lúc nào cũng ẩm ướt", 6, 1 },
                    { 22, "Đổ dầu ở vùng chữ T, hai bên má bình thường", 6, 2 },
                    { 23, "Không đổ dầu, thậm chí còn khô hơn", 6, 3 },
                    { 24, "Chỉ hơi nhờn nhẹ, nhưng không quá nhiều", 6, 4 },
                    { 25, "Rất dễ bị mụn, đặc biệt là vùng trán và cằm", 7, 1 },
                    { 26, "Thỉnh thoảng bị mụn ở vùng chữ T", 7, 2 },
                    { 27, "Hiếm khi bị mụn nhưng dễ bị kích ứng", 7, 3 },
                    { 28, "Ít bị mụn", 7, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "SkinAnswers",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "SkinQuestions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SkinQuestions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SkinQuestions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SkinQuestions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SkinQuestions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SkinQuestions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SkinQuestions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Birthday",
                table: "Users",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "SkinTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "SkinTypeName",
                value: "Da khô");

            migrationBuilder.UpdateData(
                table: "SkinTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "SkinTypeName",
                value: "Da hỗn hợp");

            migrationBuilder.InsertData(
                table: "SkinTypes",
                columns: new[] { "Id", "SkinTypeName" },
                values: new object[] { 5, "Da nhạy cảm" });
        }
    }
}
