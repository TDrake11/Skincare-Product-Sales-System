using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Skincare_Product_Sales_System.Extensions
{
	public static class AuthenticationServiceExtensions
	{
		public static IServiceCollection AddAuthenticationService(
			this IServiceCollection services,
			IConfiguration config
		)
		{
			services.AddAuthentication();
			return services;
		}
	}
}
