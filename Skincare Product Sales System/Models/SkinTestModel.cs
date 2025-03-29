using System.ComponentModel.DataAnnotations;

namespace Skincare_Product_Sales_System.Models
{
    public class SkinTestModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string SkinTestStatus { get; set; }
        public string CustomerId {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SkinTypeId { get; set; }
        public string SkinTypeName { get; set; }
    }

    public class CreateSkinTestModel
    {
        public string CustomerId { get; set; }
        public int SkinTypeId { get; set; }
    }
}
