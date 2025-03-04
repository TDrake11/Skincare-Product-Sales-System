using System.ComponentModel.DataAnnotations;

namespace Skincare_Product_Sales_System.Models
{
	public class UserProfileModel
	{
		public  string? FirstName { get; set; }
		public  string? LastName { get; set; }
		public  string? Address { get; set; }
		public  DateOnly? Birthday { get; set; }
		[EmailAddress]
		public required string Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Avatar { get; set; }

	}
}
