namespace Skincare_Product_Sales_System.Models
{
    public class SkinTypeModel
    {
        public int Id { get; set; }
        public string SkinTypeName { get; set; }
        public string SkinTypeStatus { get; set; }
    }

    public class CreateSkinTypeModel
    {
        public string SkinTypeName { get; set; }
    }

    public class UpdateSkinTypeModel
    {
        public int Id { get; set; }
        public string SkinTypeName { get; set; }
    }
}
