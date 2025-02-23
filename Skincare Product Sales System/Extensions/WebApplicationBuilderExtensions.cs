using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Infrastructure.Data;
using Skincare_Product_Sales_System_Infrastructure.Extensions;

namespace Skincare_Product_Sales_System.Extensions
{
	public static class WebApplicationBuilderExtensions
	{
		public static void AddPresentation(this WebApplicationBuilder builder, IConfiguration config)
		{
			builder.Services.AddInfrastructure(config);
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddIdentityApiEndpoints<User>()
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			builder.Services.AddAuthentication();
			builder.Services.AddAuthorization();

			builder.Services.AddSwaggerGen(c =>
			{
				c.AddSecurityDefinition("bearerAthentication", new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "bearerAthentication"
							}
						},
						[]
					}
				});
			});

		}
	}
}
