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
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = 
				options.DefaultChallengeScheme =
				options.DefaultForbidScheme =
				options.DefaultScheme = 
				options.DefaultSignInScheme =
				options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = config["JWT:Issuer"],
					ValidAudience = config["JWT:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SigningKey"]))
				};
			});	
			return services;
		}
	}
}
