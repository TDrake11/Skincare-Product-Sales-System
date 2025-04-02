using Microsoft.Extensions.FileProviders;
using Skincare_Product_Sales_System.Extensions;
using Skincare_Product_Sales_System_Domain.Entities;
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


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseCors("AllowAll");
			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(
				Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
				RequestPath = "/Uploads"
			});
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapIdentityApi<User>()
			.WithTags("A_Identity");


			app.MapControllers();

			app.Run();
		}
	}
}
