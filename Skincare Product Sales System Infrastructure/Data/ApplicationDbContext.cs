using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Skincare_Product_Sales_System_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Infrastructure.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }

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

	}
}
