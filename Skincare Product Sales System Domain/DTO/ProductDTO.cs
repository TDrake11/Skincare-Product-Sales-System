using Skincare_Product_Sales_System_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Model
{
    public class ProductDTO
    {
        public int Id { get; set; } // ID của sản phẩm
        public string ProductName { get; set; }
        public string Description { get; set; }
        public DateOnly CreatedDate { get; set; }
        public DateOnly ExpiredDate { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public string CategoryName { get; set; }
        public string? StaffName { get; set; }
        public string SkinTypeName { get; set; }
    }
}
