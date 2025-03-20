using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skincare_Product_Sales_System_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixProductStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SkinTestAnswers_SkinAnswers_AnswerId",
                table: "SkinTestAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SkinTestAnswers_SkinQuestions_QuestionId",
                table: "SkinTestAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SkinTestAnswers_SkinTests_SkinTestId",
                table: "SkinTestAnswers");

            migrationBuilder.DropColumn(
                name: "OrderDetailStatus",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "UserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "SkinTypeStatus",
                table: "SkinTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SkinTestStatus",
                table: "SkinTests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SkinTestAnswers_SkinAnswers_AnswerId",
                table: "SkinTestAnswers",
                column: "AnswerId",
                principalTable: "SkinAnswers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SkinTestAnswers_SkinQuestions_QuestionId",
                table: "SkinTestAnswers",
                column: "QuestionId",
                principalTable: "SkinQuestions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SkinTestAnswers_SkinTests_SkinTestId",
                table: "SkinTestAnswers",
                column: "SkinTestId",
                principalTable: "SkinTests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SkinTestAnswers_SkinAnswers_AnswerId",
                table: "SkinTestAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SkinTestAnswers_SkinQuestions_QuestionId",
                table: "SkinTestAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SkinTestAnswers_SkinTests_SkinTestId",
                table: "SkinTestAnswers");

            migrationBuilder.DropColumn(
                name: "SkinTypeStatus",
                table: "SkinTypes");

            migrationBuilder.DropColumn(
                name: "SkinTestStatus",
                table: "SkinTests");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "UserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ExpiredDate",
                table: "Products",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedDate",
                table: "Products",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "OrderDetailStatus",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SkinTestAnswers_SkinAnswers_AnswerId",
                table: "SkinTestAnswers",
                column: "AnswerId",
                principalTable: "SkinAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkinTestAnswers_SkinQuestions_QuestionId",
                table: "SkinTestAnswers",
                column: "QuestionId",
                principalTable: "SkinQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SkinTestAnswers_SkinTests_SkinTestId",
                table: "SkinTestAnswers",
                column: "SkinTestId",
                principalTable: "SkinTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
