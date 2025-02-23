using Skincare_Product_Sales_System_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Entities
{
	public class Product : BaseEntity
	{
		public string ProductName { get; set; }
		public string Description { get; set; }
		public DateOnly CreatedDate { get; set; }
		public DateOnly ExpiredDate { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public string? Image { get; set; }
		public string? ProductStatus { get; set; }

		[ForeignKey("Category")]
		public int? CategoryId { get; set; }
		public virtual Category? Category { get; set; } // 1 product thuoc ve 1 category

		[ForeignKey("Staff")]
		public string? StaffId { get; set; }
		public virtual User? Staff { get; set; } // 1 product chi duoc quan li boi 1 nhan vien

		[ForeignKey("SkinType")]
		public int? SkinTypeId { get; set; }
		public virtual SkinType? SkinType { get; set; } // 1 product thuoc ve 1 skin type

		//[ForeignKey("StepRoutine")]
		//public Guid? StepRoutineId { get; set; }
		//public virtual StepRoutine? StepRoutine { get; set; } // 1 product co the nam trong 1 step routine

		public virtual ICollection<Comment>? Comments { get; set; } // 1 product co nhieu comment

		public virtual ICollection<OrderDetail>? OrderDetails { get; set; } // 1 product co nhieu order detail
	}
}
