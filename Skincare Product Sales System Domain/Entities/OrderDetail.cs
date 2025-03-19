using Skincare_Product_Sales_System_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Entities
{
	public  class OrderDetail : BaseEntity
	{
		public int Quantity { get; set; }
		public double Price { get; set; }

		[ForeignKey("Order")]
		public int OrderId { get; set; }
		public virtual Order Order { get; set; } // 1 order detail chi thuoc ve 1 order

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public virtual Product Product { get; set; } // 1 order detail chi co 1 product
	}
}
