using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Infrastructure.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Order>	Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<SkinTest> SkinTests { get; set; }
		public DbSet<SkinAnswer> SkinAnswers { get; set; }
		public DbSet<SkinQuestion> SkinQuestions { get; set; }
		public DbSet<SkinTestAnswer> SkinTestAnswers { get; set; }
		public DbSet<SkinType> SkinTypes { get; set; }
		public DbSet<SkinCareRoutine> SkinCareRoutines { get; set; }
		public DbSet<StepRoutine> StepRoutines { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder); // Quan trọng: gọi base để cấu hình Identity trước

            builder.Entity<Order>()
				.HasOne(o => o.Customer)
				.WithMany() // Một User có thể có nhiều Order
				.HasForeignKey(o => o.CustomerId)
				.OnDelete(DeleteBehavior.Restrict); // Không xóa Order nếu xóa User

			builder.Entity<Order>()
				.HasOne(o => o.Staff)
				.WithMany()
				.HasForeignKey(o => o.StaffId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<SkinTestAnswer>()
			.HasOne(sta => sta.SkinTest)
			.WithMany()
			.HasForeignKey(sta => sta.SkinTestId)
			.OnDelete(DeleteBehavior.Restrict); // Không xóa SkinTest nếu có câu trả lời liên quan

			builder.Entity<SkinTestAnswer>()
				.HasOne(sta => sta.SkinQuestion)
				.WithMany()
				.HasForeignKey(sta => sta.QuestionId)
				.OnDelete(DeleteBehavior.Cascade); 

			builder.Entity<SkinTestAnswer>()
				.HasOne(sta => sta.SkinAnswer)
				.WithMany()
				.HasForeignKey(sta => sta.AnswerId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			//Remove AspNet from Identity Table Name
			foreach (var entityType in builder.Model.GetEntityTypes())
			{
				var tableName = entityType.GetTableName()!;
				if (tableName.StartsWith("AspNet"))
				{
					entityType.SetTableName(tableName.Substring(6));
				}
			}


            //builder.Entity<User>().HasData
            //(
            //    new User { Id = "377049a8-9850-4000-8691-9080973c21d1", UserName = "Aba", Email = "Nguyentu8386@gmail.com", FullName = "Nguyễn Tú", LastName = "Tú", Address = "Binh Thanh, HCM", Birthday = new DateTime(1995, 5, 20), Wallet = 500.75, Point = 120, Status = UserStatus.Active },
            //    new User { Id = "d8db7b28-7611-46f6-ab44-fc3a3f6534bd", UserName = "ABC", Email = "Hoanglam333@gmail.com", FullName = "Hoàng Lam", LastName = "Lam", Address = "Thu Duc, HCM", Birthday = new DateTime(2002, 3, 22), Wallet = 300.89, Point = 20, Status = UserStatus.Inactive },
            //    new User { Id = "4bcc0bcf-877f-41fe-9b8e-748b723c8973", UserName = "Haaa", Email = "Vietanh39@gmail.com", FullName = "Việt Anh", LastName = "Anh", Address = "Q9, HCM", Birthday = new DateTime(2000, 1, 19), Wallet = 299.95, Point = 90, Status = UserStatus.Active },
            //    new User { Id = "3a407df61-c4a7-49b6-8cb3-bad1259eaef8", UserName = "adddd", Email = "Nguyenlinh12@gmail.com", FullName = "Nguyễn Linh", LastName = "Linh", Address = "Bien Hoa, Dong Nai", Birthday = new DateTime(2003, 8, 15), Wallet = 500.99, Point = 123, Status = UserStatus.Active },
            //    new User { Id = "21ce8669-39f9-4eab-a23b-b6245cb67489", UserName = "jssww2", Email = "Vohiep09@gmail.com", FullName = "Võ Hiệp", LastName = "Hiệp", Address = "Phu Vang, Hue", Birthday = new DateTime(2001, 10, 27), Wallet = 500.75, Point = 120, Status = UserStatus.Active }
            //);

            builder.Entity<Category>().HasData
                (
                    new Category { Id = 1, CategoryName = "Tẩy trang", CategoryStatus = CategoryStatus.AVAILABEL },
                    new Category { Id = 2, CategoryName = "Sữa rửa mặt", CategoryStatus = CategoryStatus.AVAILABEL },
                    new Category { Id = 3, CategoryName = "Tẩy tế bào chết", CategoryStatus = CategoryStatus.AVAILABEL },
                    new Category { Id = 4, CategoryName = "Toner", CategoryStatus = CategoryStatus.AVAILABEL },
                    new Category { Id = 5, CategoryName = "Serum", CategoryStatus = CategoryStatus.AVAILABEL },
                    new Category { Id = 6, CategoryName = "Dưỡng ẩm", CategoryStatus = CategoryStatus.AVAILABEL },
                    new Category { Id = 7, CategoryName = "Kem chống nắng", CategoryStatus = CategoryStatus.AVAILABEL },
                    new Category { Id = 8, CategoryName = "Mặt nạ", CategoryStatus = CategoryStatus.AVAILABEL },
                    new Category { Id = 9, CategoryName = "Kem mắt", CategoryStatus = CategoryStatus.AVAILABEL }
                );

            builder.Entity<SkinType>().HasData
                (
                    new SkinType { Id = 1, SkinTypeName = "Da dầu" },
                    new SkinType { Id = 2, SkinTypeName = "Da hỗn hợp" },
                    new SkinType { Id = 3, SkinTypeName = "Da khô" },
                    new SkinType { Id = 4, SkinTypeName = "Da thường" }
                );

            builder.Entity<SkinQuestion>().HasData
                (
                    new SkinQuestion { Id = 1, QuestionText = "Khi thức dậy vào buổi sáng, bạn cảm thấy da mình thế nào?" },
                    new SkinQuestion { Id = 2, QuestionText = "Bạn có thường xuyên cảm thấy da mình bóng nhờn vào giữa ngày không?" },
                    new SkinQuestion { Id = 3, QuestionText = "Da bạn có nhiều dầu vào cuối ngày không?" },
                    new SkinQuestion { Id = 4, QuestionText = "Khi rửa mặt xong, bạn cảm thấy da mình thế nào?" },
                    new SkinQuestion { Id = 5, QuestionText = "Da bạn có hay bị bong tróc, ngứa vào mùa lạnh không?" },
                    new SkinQuestion { Id = 6, QuestionText = "Khi trời nóng, da bạn có đổ dầu nhiều hơn bình thường không?" },
                    new SkinQuestion { Id = 7, QuestionText = "Bạn có hay bị mụn không?" }
                );

            builder.Entity<SkinAnswer>().HasData
                (
                    // Trả lời cho câu 1
                    new SkinAnswer { Id = 1, AnswerText = "Đổ dầu nhiều, bóng nhờn", QuestionId =  1, SkinTypeId = 1 }, //Da dầu
                    new SkinAnswer { Id = 2, AnswerText = "Chữ T hơi dầu, má bình thường", QuestionId = 1, SkinTypeId = 2 },  //Da hỗn hợp
                    new SkinAnswer { Id = 3, AnswerText = "Khô căng, có thể bong tróc", QuestionId = 1, SkinTypeId = 3 },  //Da khô
                    new SkinAnswer { Id = 4, AnswerText = "Ổn định, không dầu cũng không khô", QuestionId = 1, SkinTypeId = 4 },   //Da thường

                    // Trả lời cho câu 2
                    new SkinAnswer { Id = 5, AnswerText = "Rất bóng, đặc biệt ở vùng trán và mũi", QuestionId = 2, SkinTypeId = 1 }, //Da dầu
                    new SkinAnswer { Id = 6, AnswerText = "Chỉ bóng ở vùng chữ T, còn lại bình thường", QuestionId = 2, SkinTypeId = 2 },  //Da hỗn hợp
                    new SkinAnswer { Id = 7, AnswerText = "Không bóng chút nào, có khi còn bong tróc", QuestionId = 2, SkinTypeId = 3 },  //Da khô
                    new SkinAnswer { Id = 8, AnswerText = "Hầu như không bóng, vẫn giữ độ ẩm tốt", QuestionId = 2, SkinTypeId = 4 },   //Da thường

                    // Trả lời cho câu 3
                    new SkinAnswer { Id = 9, AnswerText = "Rất nhiều dầu, đặc biệt là vùng chữ T", QuestionId = 3, SkinTypeId = 1 }, //Da dầu
                    new SkinAnswer { Id = 10, AnswerText = "Chỉ vùng chữ T có dầu, hai bên má thì khô", QuestionId = 3, SkinTypeId = 2 },  //Da hỗn hợp
                    new SkinAnswer { Id = 11, AnswerText = "Không có dầu, thường khô căng", QuestionId = 3, SkinTypeId = 3 },  //Da khô
                    new SkinAnswer { Id = 12, AnswerText = "Ít dầu, da luôn cân bằng", QuestionId = 3, SkinTypeId = 4 },   //Da thường

                    // Trả lời cho câu 4
                    new SkinAnswer { Id = 13, AnswerText = "Nhanh chóng đổ dầu trở lại", QuestionId = 4, SkinTypeId = 1 }, //Da dầu
                    new SkinAnswer { Id = 14, AnswerText = "Một số vùng đổ dầu, một số vùng khô", QuestionId = 4, SkinTypeId = 2 },  //Da hỗn hợp
                    new SkinAnswer { Id = 15, AnswerText = "Căng rát, thậm chí bong tróc", QuestionId = 4, SkinTypeId = 3 },  //Da khô
                    new SkinAnswer { Id = 16, AnswerText = "Mềm mại, thoải mái", QuestionId = 4, SkinTypeId = 4 },   //Da thường

                    // Trả lời cho câu 5
                    new SkinAnswer { Id = 17, AnswerText = "Không bao giờ", QuestionId = 5, SkinTypeId = 1 }, //Da dầu
                    new SkinAnswer { Id = 18, AnswerText = "Thỉnh thoảng, ở hai bên má", QuestionId = 5, SkinTypeId = 2 },  //Da hỗn hợp
                    new SkinAnswer { Id = 19, AnswerText = "KRất hay bong tróc, nhất là quanh mũi và miệng", QuestionId = 5, SkinTypeId = 3 },  //Da khô
                    new SkinAnswer { Id = 20, AnswerText = "Hiếm khi bong tróc", QuestionId = 5, SkinTypeId = 4 },   //Da thường

                    // Trả lời cho câu 6
                    new SkinAnswer { Id = 21, AnswerText = "Đổ dầu rất nhiều, da lúc nào cũng ẩm ướt", QuestionId = 6, SkinTypeId = 1 }, //Da dầu
                    new SkinAnswer { Id = 22, AnswerText = "Đổ dầu ở vùng chữ T, hai bên má bình thường", QuestionId = 6, SkinTypeId = 2 },  //Da hỗn hợp
                    new SkinAnswer { Id = 23, AnswerText = "Không đổ dầu, thậm chí còn khô hơn", QuestionId = 6, SkinTypeId = 3 },  //Da khô
                    new SkinAnswer { Id = 24, AnswerText = "Chỉ hơi nhờn nhẹ, nhưng không quá nhiều", QuestionId = 6, SkinTypeId = 4 },   //Da thường

                    // Trả lời cho câu 7
                    new SkinAnswer { Id = 25, AnswerText = "Rất dễ bị mụn, đặc biệt là vùng trán và cằm", QuestionId = 7, SkinTypeId = 1 }, //Da dầu
                    new SkinAnswer { Id = 26, AnswerText = "Thỉnh thoảng bị mụn ở vùng chữ T", QuestionId = 7, SkinTypeId = 2 },  //Da hỗn hợp
                    new SkinAnswer { Id = 27, AnswerText = "Hiếm khi bị mụn nhưng dễ bị kích ứng", QuestionId = 7, SkinTypeId = 3 },  //Da khô
                    new SkinAnswer { Id = 28, AnswerText = "Ít bị mụn", QuestionId = 7, SkinTypeId = 4 }   //Da thường
                );

            //builder.Entity<SkinTest>().HasData
            //    (
            //        new SkinTest { Id = 1, CreateDate = new DateTime(2025, 02, 14) , SkinTypeId = 1 },
            //        new SkinTest { Id = 2, CreateDate = new DateTime(2025, 02, 15), SkinTypeId = 2 },
            //    );

            //builder.Entity<SkinTestAnswer>().HasData
            //    (
            //        new SkinTestAnswer { Id = 1, SkinTestId = 1, QuestionId = 1, AnswerId = 1 },
            //        new SkinTestAnswer { Id = 2, SkinTestId = 1, QuestionId = 2, AnswerId = 5 },
            //        new SkinTestAnswer { Id = 3, SkinTestId = 1, QuestionId = 3, AnswerId = 9 },
            //        new SkinTestAnswer { Id = 4, SkinTestId = 1, QuestionId = 4, AnswerId = 15 },
            //        new SkinTestAnswer { Id = 5, SkinTestId = 1, QuestionId = 5, AnswerId = 17 },
            //        new SkinTestAnswer { Id = 6, SkinTestId = 1, QuestionId = 6, AnswerId = 22 },
            //        new SkinTestAnswer { Id = 7, SkinTestId = 1, QuestionId = 7, AnswerId = 28 },

            //        new SkinTestAnswer { Id = 8, SkinTestId = 2, QuestionId = 1, AnswerId = 2 },
            //        new SkinTestAnswer { Id = 9, SkinTestId = 2, QuestionId = 2, AnswerId = 6 },
            //        new SkinTestAnswer { Id = 10, SkinTestId = 2, QuestionId = 3, AnswerId = 10 },
            //        new SkinTestAnswer { Id = 11, SkinTestId = 2, QuestionId = 4, AnswerId = 15 },
            //        new SkinTestAnswer { Id = 12, SkinTestId = 2, QuestionId = 5, AnswerId = 17 },
            //        new SkinTestAnswer { Id = 13, SkinTestId = 2, QuestionId = 6, AnswerId = 22 },
            //        new SkinTestAnswer { Id = 14, SkinTestId = 2, QuestionId = 7, AnswerId = 28 }
            //    );




            builder.Entity<SkinCareRoutine>().HasData(
                new SkinCareRoutine { Id = 1, RoutineName = "Lộ trình cho da dầu", Description = "Giúp kiểm soát dầu, ngăn ngừa mụn và giữ ẩm nhẹ nhàng.", TotalSteps = 6, SkinTypeId = 1 },
                new SkinCareRoutine { Id = 2, RoutineName = "Lộ trình cho da khô", Description = "Dưỡng ẩm sâu, bảo vệ da khỏi bong tróc và mất nước.", TotalSteps = 6, SkinTypeId = 3 },
                new SkinCareRoutine { Id = 3, RoutineName = "Lộ trình cho da hỗn hợp", Description = "Cân bằng dầu vùng chữ T và giữ ẩm vùng khô.", TotalSteps = 6, SkinTypeId = 2 },
                new SkinCareRoutine { Id = 4, RoutineName = "Lộ trình cho da thường", Description = "Duy trì độ ẩm và bảo vệ da trước tác nhân môi trường.", TotalSteps = 6, SkinTypeId = 4 }
            );

            builder.Entity<StepRoutine>().HasData(
                // Lộ trình chăm sóc da dầu
                new StepRoutine { Id = 1, StepNumber = 1, StepDescription = "Tẩy trang để loại bỏ dầu và bụi bẩn", RoutineId = 1 },
                new StepRoutine { Id = 2, StepNumber = 2, StepDescription = "Rửa mặt với sữa rửa mặt kiềm dầu", RoutineId = 1 },
                new StepRoutine { Id = 3, StepNumber = 3, StepDescription = "Dùng toner giúp kiểm soát dầu", RoutineId = 1 },
                new StepRoutine { Id = 4, StepNumber = 4, StepDescription = "Sử dụng serum giảm dầu, ngừa mụn", RoutineId = 1 },
                new StepRoutine { Id = 5, StepNumber = 5, StepDescription = "Dưỡng ẩm nhẹ, không gây bít tắc", RoutineId = 1 },
                new StepRoutine { Id = 6, StepNumber = 6, StepDescription = "Thoa kem chống nắng kiềm dầu", RoutineId = 1 },

                // Lộ trình chăm sóc da khô
                new StepRoutine { Id = 7, StepNumber = 1, StepDescription = "Dùng dầu tẩy trang để cấp ẩm", RoutineId = 2 },
                new StepRoutine { Id = 8, StepNumber = 2, StepDescription = "Rửa mặt với sữa rửa mặt dịu nhẹ", RoutineId = 2 },
                new StepRoutine { Id = 9, StepNumber = 3, StepDescription = "Dùng toner cấp ẩm", RoutineId = 2 },
                new StepRoutine { Id = 10, StepNumber = 4, StepDescription = "Sử dụng serum cấp nước", RoutineId = 2 },
                new StepRoutine { Id = 11, StepNumber = 5, StepDescription = "Dưỡng ẩm chuyên sâu", RoutineId = 2 },
                new StepRoutine { Id = 12, StepNumber = 6, StepDescription = "Thoa kem chống nắng dưỡng ẩm", RoutineId = 2 },

                // Lộ trình chăm sóc da hỗn hợp
                new StepRoutine { Id = 13, StepNumber = 1, StepDescription = "Tẩy trang để loại bỏ dầu thừa và bụi bẩn", RoutineId = 3 },
                new StepRoutine { Id = 14, StepNumber = 2, StepDescription = "Rửa mặt với sữa rửa mặt dịu nhẹ cân bằng", RoutineId = 3 },
                new StepRoutine { Id = 15, StepNumber = 3, StepDescription = "Dùng toner giúp cân bằng da", RoutineId = 3 },
                new StepRoutine { Id = 16, StepNumber = 4, StepDescription = "Sử dụng serum dưỡng ẩm nhẹ", RoutineId = 3 },
                new StepRoutine { Id = 17, StepNumber = 5, StepDescription = "Dưỡng ẩm dạng gel hoặc lotion", RoutineId = 3 },
                new StepRoutine { Id = 18, StepNumber = 6, StepDescription = "Thoa kem chống nắng có độ ẩm vừa phải", RoutineId = 3 },

                // Lộ trình chăm sóc da thường
                new StepRoutine { Id = 19, StepNumber = 1, StepDescription = "Tẩy trang với dầu hoặc nước micellar", RoutineId = 4 },
                new StepRoutine { Id = 20, StepNumber = 2, StepDescription = "Rửa mặt với sữa rửa mặt nhẹ nhàng", RoutineId = 4 },
                new StepRoutine { Id = 21, StepNumber = 3, StepDescription = "Dùng toner cấp ẩm hoặc làm dịu da", RoutineId = 4 },
                new StepRoutine { Id = 22, StepNumber = 4, StepDescription = "Sử dụng serum tăng cường bảo vệ da", RoutineId = 4 },
                new StepRoutine { Id = 23, StepNumber = 5, StepDescription = "Dưỡng ẩm dạng kem hoặc gel phù hợp", RoutineId = 4 },
                new StepRoutine { Id = 24, StepNumber = 6, StepDescription = "Thoa kem chống nắng SPF 30+", RoutineId = 4 }
            );
             
            //builder.Entity<Product>().HasData
            //(
            //    //Nước tẩy trang
            //    new Product { Id = 1, ProductName = "Nước Tẩy Trang L'Oreal", Description = "Nước Tẩy Trang L'Oreal Tươi Mát Cho Da Dầu, Hỗn Hợp 400ml", CreatedDate = new DateOnly(2024, 1, 10), ExpiredDate = new DateOnly(2026, 1, 10), Price = 139.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 1, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 1, StepRoutineId = 1 },
            //    new Product { Id = 2, ProductName = "Nước Tẩy Trang L'Oreal", Description = "Nước Tẩy Trang L'Oreal Tươi Mát Cho Da Dầu, Hỗn Hợp 400ml", CreatedDate = new DateOnly(2024, 1, 10), ExpiredDate = new DateOnly(2026, 1, 10), Price = 139.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 1, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 2, StepRoutineId = 13 },
            //    new Product { Id = 3, ProductName = "Nước Tẩy Trang L'Oreal", Description = "Nước Tẩy Trang L'Oreal Dưỡng Ẩm Cho Da Thường, Khô 400ml", CreatedDate = new DateOnly(2024, 2, 12), ExpiredDate = new DateOnly(2026, 2, 12), Price = 199.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 1, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 3, StepRoutineId = 19 },
            //    new Product { Id = 4, ProductName = "Nước Tẩy Trang L'Oreal", Description = "Nước Tẩy Trang L'Oreal Dưỡng Ẩm Cho Da Thường, Khô 400ml", CreatedDate = new DateOnly(2024, 2, 12), ExpiredDate = new DateOnly(2026, 2, 12), Price = 199.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 1, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 4, StepRoutineId = 25 },
            //    new Product { Id = 5, ProductName = "Nước Tẩy Trang Roche-Posay", Description = "Nước Tẩy Trang Roche-Posay Kiềm Dầu 400ml", CreatedDate = new DateOnly(2024, 2, 21), ExpiredDate = new DateOnly(2026, 2, 21), Price = 199.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 1, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 1, StepRoutineId = 1 },
                
            //    //Sữa rửa mặt
            //    new Product { Id = 6, ProductName = "Sữa Rửa Mặt CeraVe", Description = "Sữa Rửa Mặt CeraVe Sạch Sâu Cho Da Thường Đến Da Dầu 473ml", CreatedDate = new DateOnly(2024, 2, 4), ExpiredDate = new DateOnly(2026, 2, 4), Price = 259.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 2, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 1, StepRoutineId = 2 },
            //    new Product { Id = 7, ProductName = "Sữa Rửa Mặt CeraVe", Description = "Sữa Rửa Mặt CeraVe Sạch Sâu Cho Da Thường Đến Da Dầu 473ml", CreatedDate = new DateOnly(2024, 2, 4), ExpiredDate = new DateOnly(2026, 2, 4), Price = 259.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 2, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 4, StepRoutineId = 20 },
            //    new Product { Id = 8, ProductName = "Sữa Rửa Mặt Cetaphil", Description = "Sữa Rửa Mặt Cetaphil Dịu Lành Cho Thường 1000ml", CreatedDate = new DateOnly(2024, 2, 14), ExpiredDate = new DateOnly(2026, 2, 14), Price = 549.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 2, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 4, StepRoutineId = 20 },
            //    new Product { Id = 9, ProductName = "Sữa Rửa Mặt Simple", Description = "Sữa Rửa Mặt Simple Dưỡng Ẩm Cho Da Khỏe Và Mịn Màng 150ml", CreatedDate = new DateOnly(2024, 2, 14), ExpiredDate = new DateOnly(2026, 2, 14), Price = 95.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 2, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 3, StepRoutineId = 8 },
            //    new Product { Id = 10, ProductName = "Sữa Rửa Mặt innisfree", Description = "Sữa Rửa Mặt innisfree Dưỡng Ẩm Chiết Xuất Trà Xanh 150g", CreatedDate = new DateOnly(2024, 2, 14), ExpiredDate = new DateOnly(2026, 2, 14), Price = 218.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 2, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 3, StepRoutineId = 8 },
            //    new Product { Id = 11, ProductName = "Gel Rửa Mặt Cosrx", Description = "Gel Rửa Mặt Cosrx Tràm Trà, 0.5% BHA Có Độ pH Thấp 150ml", CreatedDate = new DateOnly(2024, 2, 14), ExpiredDate = new DateOnly(2026, 2, 14), Price = 117.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 2, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 2, StepRoutineId = 14 },
            //    new Product { Id = 12, ProductName = "Bọt Rửa Mặt d program", Description = "Bọt Rửa Mặt d program Giúp Làm Sạch Và Cung Cấp Ẩm Da 120g", CreatedDate = new DateOnly(2024, 2, 14), ExpiredDate = new DateOnly(2026, 2, 14), Price = 228.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 2, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 2, StepRoutineId = 14 },

            //    //Tone
            //    new Product { Id = 13, ProductName = "Nước Hoa Hồng Dr.Pepti", Description = "Nước Hoa Hồng Dr.Pepti Cấp Ẩm, Căng Bóng Da 180ml", CreatedDate = new DateOnly(2024, 12, 25), ExpiredDate = new DateOnly(2026, 12, 25), Price = 275.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 4, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 3, StepRoutineId = 9 },
            //    new Product { Id = 14, ProductName = "Nước Hoa Hồng Dr.Pepti", Description = "Nước Hoa Hồng Dr.Pepti Cấp Ẩm, Căng Bóng Da 180ml", CreatedDate = new DateOnly(2024, 12, 25), ExpiredDate = new DateOnly(2026, 12, 25), Price = 275.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 4, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 4, StepRoutineId = 21 },
            //    new Product { Id = 15, ProductName = "Toner Pyunkang Yul", Description = "Toner Pyunkang Yul Cấp Ẩm, Làm Dịu Da 200ml", CreatedDate = new DateOnly(2024, 12, 25), ExpiredDate = new DateOnly(2026, 12, 25), Price = 275.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 4, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 3, StepRoutineId = 9 },
            //    new Product { Id = 16, ProductName = "Toner Pyunkang Yul", Description = "Toner Pyunkang Yul Cấp Ẩm, Làm Dịu Da 200ml", CreatedDate = new DateOnly(2024, 12, 25), ExpiredDate = new DateOnly(2026, 12, 25), Price = 275.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 4, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 4, StepRoutineId = 21 },
            //    new Product { Id = 17, ProductName = "Toner SVR Sebiaclear", Description = "Toner SVR Sebiaclear Dành Cho Da Dầu, Mụn 150ml", CreatedDate = new DateOnly(2024, 12, 25), ExpiredDate = new DateOnly(2026, 12, 25), Price = 318.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 4, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 1, StepRoutineId = 3 },
            //    new Product { Id = 18, ProductName = "Toner SVR Sebiaclear", Description = "Toner SVR Sebiaclear Sạch Da, Thông Thoán Lỗ Chân Lông 150ml", CreatedDate = new DateOnly(2024, 12, 25), ExpiredDate = new DateOnly(2026, 12, 25), Price = 318.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 4, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 4, StepRoutineId = 21 },

            //    //Serum
            //    new Product { Id = 19, ProductName = "Serum La Roche-Posay", Description = "Serum La Roche-Posay Giảm Thâm Nám & Dưỡng Sáng Da 30ml", CreatedDate = new DateOnly(2024, 12, 12), ExpiredDate = new DateOnly(2026, 12, 12), Price = 866.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 5, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 4, StepRoutineId = 22 },
            //    new Product { Id = 20, ProductName = "Serum L'Oreal", Description = "Serum L'Oreal Hyaluronic Acid Cấp Ẩm Sáng Da 30ml", CreatedDate = new DateOnly(2024, 12, 16), ExpiredDate = new DateOnly(2026, 12, 16), Price = 298.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 5, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 4, StepRoutineId = 22 },
            //    new Product { Id =21, ProductName = "Serum Tia'm", Description = "Serum Tia'm Thu Nhỏ Lỗ Chân Lông, Giảm Dầu Nhờn 40m", CreatedDate = new DateOnly(2024, 12, 22), ExpiredDate = new DateOnly(2026, 12, 22), Price = 339.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 5, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 1, StepRoutineId = 4 },
            //    new Product { Id = 22, ProductName = "Serum Derladie", Description = "Serum Derladie Niacinamide 20% Giảm Mụn, Mờ Thâm Đỏ 30ml", CreatedDate = new DateOnly(2024, 12, 22), ExpiredDate = new DateOnly(2026, 12, 22), Price = 280.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 5, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 1, StepRoutineId = 4 },
            //    new Product { Id = 23, ProductName = "Serum Bí Đao Cocoon", Description = "Serum Bí Đao Cocoon Làm Giảm Mụn, Mờ Thâm 70ml", CreatedDate = new DateOnly(2024, 12, 12), ExpiredDate = new DateOnly(2026, 12, 12), Price = 415.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 5, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 2, StepRoutineId = 16 },
            //    new Product { Id = 24, ProductName = "Serum Garnier", Description = "Serum Garnier Giảm Mụn Mờ Thâm Cho Da Dầu, Mụn 30ml", CreatedDate = new DateOnly(2024, 12, 16), ExpiredDate = new DateOnly(2026, 12, 16), Price = 298.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 5, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 2, StepRoutineId = 16 },
            //    new Product { Id = 25, ProductName = "Serum 9Wishes", Description = "Serum 9Wishes Dưỡng Ẩm & Làm Căng Bóng Da 30ml", CreatedDate = new DateOnly(2024, 12, 22), ExpiredDate = new DateOnly(2026, 12, 22), Price = 177.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 5, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 3, StepRoutineId = 10 },
            //    new Product { Id = 26, ProductName = "Serum Klairs", Description = "Serum Klairs Cấp Ẩm Cho Da Khô, Nhạy Cảm 80ml", CreatedDate = new DateOnly(2024, 12, 22), ExpiredDate = new DateOnly(2026, 12, 22), Price = 286.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 5, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 3, StepRoutineId = 10 },

            //    //Dưỡng ẩm                 
            //    new Product { Id = 27, ProductName = "Kem Dưỡng Olay Luminous", Description = "Kem Dưỡng Olay Luminous Sáng Da Mờ Thâm Nám Ban Đêm 50g", CreatedDate = new DateOnly(2024, 12, 12), ExpiredDate = new DateOnly(2026, 12, 12), Price = 866.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 6, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 4, StepRoutineId = 23 },
            //    new Product { Id = 28, ProductName = "Sữa Dưỡng Ẩm Embryolisse", Description = "Sữa Dưỡng Ẩm Embryolisse Siêu Phục Hồi Da 75ml", CreatedDate = new DateOnly(2024, 12, 16), ExpiredDate = new DateOnly(2026, 12, 16), Price = 298.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 6, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 3, StepRoutineId = 11 },
            //    new Product { Id = 29, ProductName = "Kem Dưỡng Bioderma", Description = "Kem Dưỡng Bioderma Giúp Se Khít Lỗ Chân Lông 30ml", CreatedDate = new DateOnly(2024, 12, 22), ExpiredDate = new DateOnly(2026, 12, 22), Price = 339.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 6, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 2, StepRoutineId = 17 },
            //    new Product { Id = 30, ProductName = "Kem Dưỡng SVR", Description = "Kem Dưỡng SVR Kiềm Dầu, Se Khít Lỗ Chân Lông 40ml", CreatedDate = new DateOnly(2024, 12, 22), ExpiredDate = new DateOnly(2026, 12, 22), Price = 280.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 6, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 1, StepRoutineId = 5 },
                
            //    //Kem chống nắng                
            //    new Product { Id = 31, ProductName = "Kem Chống Nắng L'Oreal", Description = "Kem Chống Nắng L'Oreal Paris X20 Thoáng Da Mỏng Nhẹ 50ml", CreatedDate = new DateOnly(2024, 12, 12), ExpiredDate = new DateOnly(2026, 12, 12), Price = 415.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 7, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 4, StepRoutineId = 24 },
            //    new Product { Id = 32, ProductName = "Sữa Chống Nắng Anessa", Description = "Sữa Chống Nắng Anessa Dưỡng Da Kiềm Dầu 60ml ", CreatedDate = new DateOnly(2024, 12, 16), ExpiredDate = new DateOnly(2026, 12, 16), Price = 298.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 7, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 1, StepRoutineId = 6 },
            //    new Product { Id = 33, ProductName = "Kem Chống Nắng Beplain ", Description = "Kem Chống Nắng Beplain Nâng Tông, Kiềm Dầu Mịn Lì 50ml", CreatedDate = new DateOnly(2024, 12, 22), ExpiredDate = new DateOnly(2026, 12, 22), Price = 177.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 7, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 2, StepRoutineId = 18 },
            //    new Product { Id = 34, ProductName = "Tinh Chất Chống Nắng Sunplay", Description = "Tinh Chất Chống Nắng Sunplay Hiệu Chỉnh Sắc Da 50g", CreatedDate = new DateOnly(2024, 12, 22), ExpiredDate = new DateOnly(2026, 12, 22), Price = 286.00, Quantity = 100, Image = "serum_vitc.jpg", ProductStatus = ProductStatus.Active, CategoryId = 7, StaffId = "377049a8-9850-4000-8691-9080973c21d1", SkinTypeId = 3, StepRoutineId = 12 }
            //);

            //builder.Entity<Order>().HasData
            //    (
            //        new Order { Id = 1, OrderDate = new DateTime(2025, 2, 15), TotalPrice = 561.00, OrderStatus = OrderStatus.Pending },
            //        new Order { Id = 2, OrderDate = new DateTime(2025, 2, 15), TotalPrice = 415.00, OrderStatus = OrderStatus.Pending },
            //        new Order { Id = 3, OrderDate = new DateTime(2025, 2, 16), TotalPrice = 866.00, OrderStatus = OrderStatus.Canceled}
            //    );

            //builder.Entity<OrderDetail>().HasData
            //    (
            //        new OrderDetail { Id = 1, Quantity = 1, Price = 286.00, OrderId = 1, ProductId = 34 },
            //        new OrderDetail { Id = 2, Quantity = 1, Price = 275.00, OrderId = 1, ProductId = 13 },
            //        new OrderDetail { Id = 3, Quantity = 1, Price = 415.00, OrderId = 2, ProductId = 31 },
            //        new OrderDetail { Id = 4, Quantity = 1, Price = 866.00, OrderId = 3, ProductId = 19 }
            //    );
        }
	}
}
