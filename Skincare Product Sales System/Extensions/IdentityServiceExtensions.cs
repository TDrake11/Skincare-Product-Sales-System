using Microsoft.AspNetCore.Identity;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Infrastructure.Data;

namespace Skincare_Product_Sales_System.Extensions
{
	public static class IdentityServiceExtensions
	{
		public static IServiceCollection AddIdentityService(
			this IServiceCollection services,
			IConfiguration config
		)
		{
			// Đăng ký các dịch vụ của Identity
			services
				.AddIdentityApiEndpoints<User>()
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			// Truy cập IdentityOptions
			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = false; // Không có ký tự số
				options.Password.RequireLowercase = false; // Không có ký tự thường
				options.Password.RequireUppercase = false; // Không có ký tự hoa
				options.Password.RequireNonAlphanumeric = false; // Không có ký tự đặc biệt
				options.Password.RequiredLength = 8; // Độ dài tối thiểu là 8 ký tự

				// Cấu hình Lockout - khóa user
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); // Khóa 1 phút
				options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lần thì khóa
				options.Lockout.AllowedForNewUsers = true;

				// Cấu hình về User.
				options.User.RequireUniqueEmail = true; // Email là duy nhất

				// Cấu hình đăng nhập.
				options.SignIn.RequireConfirmedEmail = true; // Cấu hình xác thực địa chỉ email (email phải tồn tại)
				options.SignIn.RequireConfirmedPhoneNumber = false; // Xác thực số điện thoại
			});

			return services;
		}
	}
}
