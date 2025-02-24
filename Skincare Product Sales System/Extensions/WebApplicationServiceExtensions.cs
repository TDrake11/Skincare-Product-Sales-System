﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Skincare_Product_Sales_System.Helpers;
using Skincare_Product_Sales_System_Application.Services.ProductService;
using Skincare_Product_Sales_System_Application.Services.TokenService;
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
			services.AddAuthenticationService(config);
			services.AddAuthorization();

			services.AddSwaggerGen(option =>
			{
				option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
				option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter a valid token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});
				option.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type=ReferenceType.SecurityScheme,
								Id="Bearer"
							}
						},
						[]
					}
				});
			});

			services.AddAutoMapper(typeof(MappingProfile));
			services.AddAutoMapper(typeof(MappingProfile).Assembly);

			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IProductService, ProductService>();

			return services;
		}
	}
}
