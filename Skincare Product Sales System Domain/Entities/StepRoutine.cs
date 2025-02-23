using Skincare_Product_Sales_System_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Entities
{
	public  class StepRoutine : BaseEntity
	{
		public int StepNumber { get; set; }
		public string StepDescription { get; set; }

		public string Status { get; set; }

		[ForeignKey("Routine")]
		public int? RoutineId { get; set; }
		public virtual SkinCareRoutine? Routine { get; set; } // 1 routine product chi co 1 routine		

		[ForeignKey("Product")]
		public int? ProductId { get; set; }
		public virtual Product? Product { get; set; } // 1 step routine co the co 1 product

		//public virtual ICollection<Product>? Products { get; set; } // 1 step routine co the co nhieu product
	}
}
