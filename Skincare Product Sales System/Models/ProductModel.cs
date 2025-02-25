namespace Skincare_Product_Sales_System.Models
{
	public class ProductModel
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
		public string Description { get; set; }
		public DateOnly CreatedDate { get; set; }
		public DateOnly ExpiredDate { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public string? Image { get; set; }
		public string? ProductStatus { get; set; }
		public string CategoryName { get; set; }
		public string SkinTypeName { get; set; }
	}
}
