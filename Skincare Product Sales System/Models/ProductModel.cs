namespace Skincare_Product_Sales_System.Models
{
	public class ProductModel
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
		public string Description { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ExpiredDate { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public string? Image { get; set; }
		public string? ProductStatus { get; set; }
		public int? CategoryId { get; set; }
		public string? CategoryName { get; set; }
		public int? SkinTypeId { get; set; }
		public string? SkinTypeName { get; set; }
	}

    public class ProductUpdateModel
    {
		public int Id { get; set; }
		public string ProductName { get; set; }
		public string Description { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ExpiredDate { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public string? Image { get; set; }
		public string? ProductStatus { get; set; }
		public int? CategoryId { get; set; }
		public int? SkinTypeId { get; set; }
	}
}
