using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Skincare_Product_Sales_System_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkinQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkinTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkinTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wallet = table.Column<double>(type: "float", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkinAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    SkinTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkinAnswers_SkinQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "SkinQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkinAnswers_SkinTypes_SkinTypeId",
                        column: x => x.SkinTypeId,
                        principalTable: "SkinTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkinCareRoutines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoutineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalSteps = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SkinTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinCareRoutines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkinCareRoutines_SkinTypes_SkinTypeId",
                        column: x => x.SkinTypeId,
                        principalTable: "SkinTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StaffId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkinTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SkinTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkinTests_SkinTypes_SkinTypeId",
                        column: x => x.SkinTypeId,
                        principalTable: "SkinTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkinTests_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StepRoutines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepNumber = table.Column<int>(type: "int", nullable: false),
                    StepDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RoutineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepRoutines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StepRoutines_SkinCareRoutines_RoutineId",
                        column: x => x.RoutineId,
                        principalTable: "SkinCareRoutines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkinTestAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkinTestId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: true),
                    AnswerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinTestAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkinTestAnswers_SkinAnswers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "SkinAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkinTestAnswers_SkinQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "SkinQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkinTestAnswers_SkinTests_SkinTestId",
                        column: x => x.SkinTestId,
                        principalTable: "SkinTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpiredDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductStatus = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SkinTypeId = table.Column<int>(type: "int", nullable: false),
                    StepRoutineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_SkinTypes_SkinTypeId",
                        column: x => x.SkinTypeId,
                        principalTable: "SkinTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_StepRoutines_StepRoutineId",
                        column: x => x.StepRoutineId,
                        principalTable: "StepRoutines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Users_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentStatus = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CategoryStatus" },
                values: new object[,]
                {
                    { 1, "Tẩy trang", 1 },
                    { 2, "Sữa rửa mặt", 1 },
                    { 3, "Tẩy tế bào chết", 1 },
                    { 4, "Toner", 1 },
                    { 5, "Serum", 1 },
                    { 6, "Dưỡng ẩm", 1 },
                    { 7, "Kem chống nắng", 1 },
                    { 8, "Mặt nạ", 1 },
                    { 9, "Kem mắt", 1 }
                });

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

            migrationBuilder.InsertData(
                table: "SkinTypes",
                columns: new[] { "Id", "SkinTypeName" },
                values: new object[,]
                {
                    { 1, "Da dầu" },
                    { 2, "Da hỗn hợp" },
                    { 3, "Da khô" },
                    { 4, "Da thường" }
                });

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

            migrationBuilder.InsertData(
                table: "SkinCareRoutines",
                columns: new[] { "Id", "Description", "RoutineName", "SkinTypeId", "Status", "TotalSteps" },
                values: new object[,]
                {
                    { 1, "Giúp kiểm soát dầu, ngăn ngừa mụn và giữ ẩm nhẹ nhàng.", "Lộ trình cho da dầu", 1, 0, 6 },
                    { 2, "Dưỡng ẩm sâu, bảo vệ da khỏi bong tróc và mất nước.", "Lộ trình cho da khô", 3, 0, 6 },
                    { 3, "Cân bằng dầu vùng chữ T và giữ ẩm vùng khô.", "Lộ trình cho da hỗn hợp", 2, 0, 6 },
                    { 4, "Duy trì độ ẩm và bảo vệ da trước tác nhân môi trường.", "Lộ trình cho da thường", 4, 0, 6 }
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
                    { 24, 4, 0, "Thoa kem chống nắng SPF 30+", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CustomerId",
                table: "Comments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StaffId",
                table: "Orders",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SkinTypeId",
                table: "Products",
                column: "SkinTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StaffId",
                table: "Products",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StepRoutineId",
                table: "Products",
                column: "StepRoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SkinAnswers_QuestionId",
                table: "SkinAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SkinAnswers_SkinTypeId",
                table: "SkinAnswers",
                column: "SkinTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SkinCareRoutines_SkinTypeId",
                table: "SkinCareRoutines",
                column: "SkinTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SkinTestAnswers_AnswerId",
                table: "SkinTestAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_SkinTestAnswers_QuestionId",
                table: "SkinTestAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SkinTestAnswers_SkinTestId",
                table: "SkinTestAnswers",
                column: "SkinTestId");

            migrationBuilder.CreateIndex(
                name: "IX_SkinTests_CustomerId",
                table: "SkinTests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SkinTests_SkinTypeId",
                table: "SkinTests",
                column: "SkinTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StepRoutines_RoutineId",
                table: "StepRoutines",
                column: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "SkinTestAnswers");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SkinAnswers");

            migrationBuilder.DropTable(
                name: "SkinTests");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "StepRoutines");

            migrationBuilder.DropTable(
                name: "SkinQuestions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SkinCareRoutines");

            migrationBuilder.DropTable(
                name: "SkinTypes");
        }
    }
}
