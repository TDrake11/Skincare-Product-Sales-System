using System.ComponentModel.DataAnnotations;

namespace Skincare_Product_Sales_System.Models
{
	public class RegisterModel
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Address { get; set; }
		public required DateOnly Birthday { get; set; }
		[EmailAddress]
		public required string Email { get; set; }
		public required string Password { get; set; }		
	}
}
