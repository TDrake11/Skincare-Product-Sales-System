
using Microsoft.AspNetCore.Identity;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Infrastructure.Data;
using Skincare_Product_Sales_System_Infrastructure.Extensions;

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
			services.AddAuthentication();

			builder.Services.AddControllers();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

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
