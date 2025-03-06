using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Entities
{
	public class SkinType : BaseEntity
	{
		public string SkinTypeName { get; set; }
		public string SkinTypeStatus { get; set; }
		public virtual ICollection<Product>? Products { get; set; } // 1 skin type co nhieu product
		public virtual ICollection<SkinCareRoutine>? SkinCareRoutines { get; set; } // 1 skin type co nhieu routine
		public virtual ICollection<SkinTest>? SkinTests { get; set; } // 1 skin type co nhieu skin test
		public virtual ICollection<SkinAnswer>? SkinAnswer { get; set; } // 1 skin type co nhieu skin answer
	}
}
