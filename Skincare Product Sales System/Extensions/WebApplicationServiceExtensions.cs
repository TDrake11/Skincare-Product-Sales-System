using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Skincare_Product_Sales_System.Helpers;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Interfaces;
using Skincare_Product_Sales_System_Infrastructure.Data;
using Skincare_Product_Sales_System_Infrastructure.Extensions;

namespace Skincare_Product_Sales_System.Extensions
{
	public static class WebApplicationServiceExtensions
	{
		public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config)
		{
			services.AddInfrastructure(config);
			services.AddControllers();
			services.AddEndpointsApiExplorer();
			//builder.Services.AddIdentityApiEndpoints<User>()
			//	.AddRoles<IdentityRole>()
			//	.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddAuthorization();

			services.AddSwaggerGen();

			services.AddAutoMapper(typeof(MappingProfile));
			services.AddAutoMapper(typeof(MappingProfile).Assembly);

			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}
}
