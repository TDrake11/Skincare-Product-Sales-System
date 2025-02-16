using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Enums
{
	public enum OrderStatus
	{
        Pending = 0,     // Đang chờ xử lý
        Completed = 1,   // Đã hoàn thành
        Canceled = 2     // Đã hủy
    }
}
