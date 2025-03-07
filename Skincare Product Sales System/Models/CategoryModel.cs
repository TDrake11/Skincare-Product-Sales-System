namespace Skincare_Product_Sales_System.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CategoryStatus { get; set; }
    }

    // Hàm không chứa CategoryStatus
    public class CreateCategoryRequest
    {
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class UpdateCategoryRequest
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
