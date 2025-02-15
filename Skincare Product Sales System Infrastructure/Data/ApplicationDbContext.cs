using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Skincare_Product_Sales_System_Domain.Entities;
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


            builder.Entity<User>().HasData
            (
                new User { Id = "377049a8-9850-4000-8691-9080973c21d1", UserName = "Aba", Email = "Nguyentu8386@gmail.com", FullName = "Nguyễn Tú", LastName = "Tú", Address = "Binh Thanh, HCM", Birthday = new DateTime(1995, 5, 20), Wallet = 500.75, Point = 120, Status = "Active" },
                new User { Id = "d8db7b28-7611-46f6-ab44-fc3a3f6534bd", UserName = "ABC", Email = "Hoanglam333@gmail.com", FullName = "Hoàng Lam", LastName = "Lam", Address = "Thu Duc, HCM", Birthday = new DateTime(2002, 3, 22), Wallet = 300.89, Point = 20, Status = "Active" },
                new User { Id = "4bcc0bcf-877f-41fe-9b8e-748b723c8973", UserName = "Haaa", Email = "Vietanh39@gmail.com", FullName = "Việt Anh", LastName = "Anh", Address = "Q9, HCM", Birthday = new DateTime(2000, 1, 19), Wallet = 299.95, Point = 90, Status = "Active" },
                new User { Id = "3a407df61-c4a7-49b6-8cb3-bad1259eaef8", UserName = "adddd", Email = "Nguyenlinh12@gmail.com", FullName = "Nguyễn Linh", LastName = "Linh", Address = "Bien Hoa, Dong Nai", Birthday = new DateTime(2003, 8, 15), Wallet = 500.99, Point = 123, Status = "Active" },
                new User { Id = "21ce8669-39f9-4eab-a23b-b6245cb67489", UserName = "jssww2", Email = "Vohiep09@gmail.com", FullName = "Võ Hiệp", LastName = "Hiệp", Address = "Phu Vang, Hue", Birthday = new DateTime(2001, 10, 27), Wallet = 500.75, Point = 120, Status = "Active" }
            );




            builder.Entity<Category>().HasData
                (
                    new Category { Id = 1, CategoryName = "Tẩy trang" },
                    new Category { Id = 2, CategoryName = "Sữa rửa mặt" },
                    new Category { Id = 3, CategoryName = "Tẩy tế bào chết" },
                    new Category { Id = 4, CategoryName = "Toner" },
                    new Category { Id = 5, CategoryName = "Serum" },
                    new Category { Id = 6, CategoryName = "Dưỡng ẩm" },
                    new Category { Id = 7, CategoryName = "Kem chống nắng" },
                    new Category { Id = 8, CategoryName = "Mặt nạ" },
                    new Category { Id = 9, CategoryName = "Kem mắt" }
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

            builder.Entity<SkinCareRoutine>().HasData(
                new SkinCareRoutine { Id = 1, RoutineName = "Lộ trình cho da dầu", Description = "Giúp kiểm soát dầu, ngăn ngừa mụn và giữ ẩm nhẹ nhàng.", TotalSteps = 6, SkinTypeId = 1 },
                new SkinCareRoutine { Id = 2, RoutineName = "Lộ trình cho da khô", Description = "Dưỡng ẩm sâu, bảo vệ da khỏi bong tróc và mất nước.", TotalSteps = 6, SkinTypeId = 2 },
                new SkinCareRoutine { Id = 3, RoutineName = "Lộ trình cho da hỗn hợp", Description = "Cân bằng dầu vùng chữ T và giữ ẩm vùng khô.", TotalSteps = 6, SkinTypeId = 3 },
                new SkinCareRoutine { Id = 4, RoutineName = "Lộ trình cho da thường", Description = "Duy trì độ ẩm và bảo vệ da trước tác nhân môi trường.", TotalSteps = 6, SkinTypeId = 4 },
                new SkinCareRoutine { Id = 5, RoutineName = "Lộ trình cho da nhạy cảm", Description = "Làm dịu da, giảm kích ứng và tăng cường hàng rào bảo vệ.", TotalSteps = 6, SkinTypeId = 5 }
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
                new StepRoutine { Id = 24, StepNumber = 6, StepDescription = "Thoa kem chống nắng SPF 30+", RoutineId = 4 },

                // Lộ trình chăm sóc da nhạy cảm
                new StepRoutine { Id = 25, StepNumber = 1, StepDescription = "Tẩy trang dịu nhẹ, tránh sản phẩm có cồn", RoutineId = 5 },
                new StepRoutine { Id = 26, StepNumber = 2, StepDescription = "Rửa mặt với sữa rửa mặt không chứa hương liệu", RoutineId = 5 },
                new StepRoutine { Id = 27, StepNumber = 3, StepDescription = "Dùng toner không chứa cồn", RoutineId = 5 },
                new StepRoutine { Id = 28, StepNumber = 4, StepDescription = "Sử dụng serum phục hồi da", RoutineId = 5 },
                new StepRoutine { Id = 29, StepNumber = 5, StepDescription = "Dưỡng ẩm chuyên biệt cho da nhạy cảm", RoutineId = 5 },
                new StepRoutine { Id = 30, StepNumber = 6, StepDescription = "Thoa kem chống nắng vật lý dịu nhẹ", RoutineId = 5 }
            );


        }
	}
}
