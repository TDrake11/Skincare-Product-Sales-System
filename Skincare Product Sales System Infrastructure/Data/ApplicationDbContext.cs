using Microsoft.AspNetCore.Identity;
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
		}
	}
}
