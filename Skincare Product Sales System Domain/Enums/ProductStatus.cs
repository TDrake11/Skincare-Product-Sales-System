using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Enums
{
	public enum ProductStatus
	{
        Active = 0,       // Sản phẩm đang hoạt động
        OutOfStock = 1,   // Hết hàng
        Expired = 2       // Hết hạn sử dụng		
    }
}
