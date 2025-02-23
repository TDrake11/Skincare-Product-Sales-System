using Skincare_Product_Sales_System_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Entities
{
	public class SkinCareRoutine : BaseEntity
	{
		public string RoutineName { get; set; }
		public string Description { get; set; }
		public int TotalSteps { get; set; }
		public string Status { get; set; }

		[ForeignKey("SkinType")]
		public int? SkinTypeId { get; set; }
		public virtual SkinType? SkinType { get; set; } // 1 routine chi thuoc ve 1 skin type
		public virtual ICollection<StepRoutine>? StepRoutines { get; set; } // 1 routine co nhieu step routine

	}
}
