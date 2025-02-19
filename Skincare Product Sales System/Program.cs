
using Microsoft.AspNetCore.Identity;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Interfaces;
using Skincare_Product_Sales_System_Domain.Repositories;
using Skincare_Product_Sales_System_Infrastructure.Data;
using Skincare_Product_Sales_System_Infrastructure.Extensions;
using Skincare_Product_Sales_System_Infrastructure.Repositories;

namespace Skincare_Product_Sales_System
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var services = builder.Services;
			var config = builder.Configuration;

			// Add services to the container.
			services.AddInfrastructure(config);
			services.AddIdentityApiEndpoints<User>()
			.AddEntityFrameworkStores<ApplicationDbContext>();
			services.AddAuthorization();

			builder.Services.AddControllers();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapIdentityApi<User>();

			app.MapControllers();

			//await app.AddAutoMigrateDatabase();

			app.Run();
		}
	}
}
