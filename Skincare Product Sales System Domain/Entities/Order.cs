using Skincare_Product_Sales_System_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Entities
{
	public class Order : BaseEntity
	{
		public DateTime OrderDate { get; set; }
		public double TotalPrice { get; set; }
		public OrderStatus OrderStatus { get; set; }

		[ForeignKey("Customer")]
		public string CustomerId { get; set; } // id cua khach hang dat hang
		public virtual User Customer { get; set; } // 1 oder chi duoc dat hang boi 1 khach hang

		[ForeignKey("Staff")]
		public string? StaffId { get; set; } // id cua nhan vien xu ly don hang,
		public virtual User? Staff { get; set; } // 1 don hang chỉ có 1 nhan vien xu ly

		public virtual ICollection<OrderDetail>? OrderDetails { get; set; } // 1 order co nhieu order detail
	}
}
