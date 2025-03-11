using Skincare_Product_Sales_System_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Entities
{
	public class Category : BaseEntity
	{
		public string CategoryName { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CategoryStatus { get; set; }
		//public virtual ICollection<Product>? Products { get; set; } // 1 category co nhieu productz
	}
}
