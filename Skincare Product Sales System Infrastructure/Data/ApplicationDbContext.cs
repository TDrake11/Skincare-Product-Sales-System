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

            builder.Entity<Category>().HasData(
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

            builder.Entity<SkinType>().HasData(
				new SkinType { Id = 1, SkinTypeName = "Da dầu" },
				new SkinType { Id = 2, SkinTypeName = "Da khô" },
				new SkinType { Id = 3, SkinTypeName = "Da hỗn hợp" },
				new SkinType { Id = 4, SkinTypeName = "Da thường" },
				new SkinType { Id = 5, SkinTypeName = "Da nhạy cảm" }
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
		}
	}
}
