using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skincare_Product_Sales_System_Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skincare_Product_Sales_System_Domain.Entities;

namespace Skincare_Product_Sales_System_Infrastructure.Extensions
{
	public static class ApplicationServiceExtension
	{
		public static void AddInfrastructure (this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
		}
	}
}
