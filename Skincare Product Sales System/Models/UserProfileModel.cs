using System.ComponentModel.DataAnnotations;

namespace Skincare_Product_Sales_System.Models
{
	public class UserProfileModel
	{
		public  string? FirstName { get; set; }
		public  string? LastName { get; set; }
		public  string? Address { get; set; }
		public  DateOnly? Birthday { get; set; }
		public string? Wallet { get; set; }
		public double? Point { get; set; }
		[EmailAddress]
		public required string Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Avatar { get; set; }
		public string? RoleName { get; set; }
	}
	public class UserProfileUpdateModel
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Address { get; set; }
		public DateOnly? Birthday { get; set; }
		[EmailAddress]
		public required string Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Avatar { get; set; }
	}
	public class RegisterModel
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Address { get; set; }
		public required DateOnly Birthday { get; set; }
		public string? PhoneNumber { get; set; }
		[EmailAddress]
		public required string Email { get; set; }
		public required string Password { get; set; }
	}

	public class LoginModel
	{
		public required string Email { get; set; }
		public required string Password { get; set; }
	}

	public class UserTokenModel
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Token { get; set; }
	}
}
