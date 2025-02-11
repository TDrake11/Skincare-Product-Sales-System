using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skincare_Product_Sales_System_Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Microsoft.Extensions.Hosting;

namespace Skincare_Product_Sales_System_Infrastructure.Extensions
{
	public static class ApplicationServiceExtension
	{
		public static IServiceCollection AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
			return services;
		}

		public static async Task<IHost> AddAutoMigrateDatabase(this IHost app)
		{
			using var scope = app.Services.CreateScope();
			var serviceProvider = scope.ServiceProvider;
			var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
			try
			{
				var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
				await context.Database.MigrateAsync();
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "An error occur during migration");
			}

			return app;
		}
	}
}
