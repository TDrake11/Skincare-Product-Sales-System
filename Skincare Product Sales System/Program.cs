
using Microsoft.AspNetCore.Identity;
using Skincare_Product_Sales_System.Extensions;
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

			services.AddCors(options =>
			{
				options.AddPolicy("AllowAll",
					policy =>
					{
						policy.AllowAnyOrigin()
							  .AllowAnyMethod()
							  .AllowAnyHeader();
					});
			});

			services.AddPresentation(config);
			services.AddIdentityService(config);
			services.AddAuthenticationService(config);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseCors("AllowAll");
			app.UseAuthentication();
			app.UseAuthorization();

			//app.MapIdentityApi<User>();

			app.MapControllers();

			//await app.AddAutoMigrateDatabase();

			app.Run();
		}
	}
}
