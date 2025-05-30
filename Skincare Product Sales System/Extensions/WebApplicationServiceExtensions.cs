﻿using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Skincare_Product_Sales_System.Helpers;
using Skincare_Product_Sales_System_Application.Service.StepRoutineService;
using Skincare_Product_Sales_System_Application.Services.CategoryService;
using Skincare_Product_Sales_System_Application.Services.CommentService;
using Skincare_Product_Sales_System_Application.Services.OrderDetailService;
using Skincare_Product_Sales_System_Application.Services.OrderService;
using Skincare_Product_Sales_System_Application.Services.ProductService;
using Skincare_Product_Sales_System_Application.Services.SkinAnswerService;
using Skincare_Product_Sales_System_Application.Services.SkinCareRoutineService;
using Skincare_Product_Sales_System_Application.Services.SkinQuestionService;
using Skincare_Product_Sales_System_Application.Services.SkinTestAnswerService;
using Skincare_Product_Sales_System_Application.Services.SkinTestService;
using Skincare_Product_Sales_System_Application.Services.SkinTypeService;
using Skincare_Product_Sales_System_Application.Services.StepRoutineServices;
using Skincare_Product_Sales_System_Application.Services.TokenService;
using Skincare_Product_Sales_System_Domain.Interfaces;
using Skincare_Product_Sales_System_Infrastructure.Data;

namespace Skincare_Product_Sales_System.Extensions
{
	public static class WebApplicationServiceExtensions
	{
		public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config)
		{
			services.AddControllers();
			services.AddEndpointsApiExplorer();
			services.AddAuthenticationService(config);
			services.AddAuthorization();

			services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer"
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					new string[] { }
				}
			});
			});

			services.AddAutoMapper(typeof(MappingProfile));
			services.AddAutoMapper(typeof(MappingProfile).Assembly);

			// Đăng ký DbContext với vòng đời Scoped
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

			// Các service khác
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<ITokenService, TokenService>();

			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<ICommentService, CommentService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IOrderDetailService, OrderDetailService>();
			services.AddScoped<ISkinQuestionService, SkinQuestionService>();
			services.AddScoped<ISkinAnswerService, SkinAnswerService>();
			services.AddScoped<ISkinTypeService, SkinTypeService>();
			services.AddScoped<ISkinTestService, SkinTestService>();
			services.AddScoped<ISkinTestAnswerService, SkinTestAnswerService>();
			services.AddScoped<ISkinCareRoutineService, SkinCareRoutineService>();
			services.AddScoped<IStepRoutineService, StepRoutineService>();

			return services;
		}
	}

}
