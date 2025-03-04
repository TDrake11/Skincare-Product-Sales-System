using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Entities
{
	public class SkinTest : BaseEntity
	{
		public DateTime CreateDate { get; set; }
		public string SkinTestStatus { get; set; }

		[ForeignKey("Customer")]
		public string? CustomerId { get; set; }
		public virtual User? Customer { get; set; } // 1 skin test chi thuoc ve 1 khach hang

		[ForeignKey("SkinType")]
		public int?	 SkinTypeId { get; set; }
		public virtual SkinType? SkinType { get; set; } // 1 skin test chi thuoc ve 1 skin type

		//public virtual ICollection<SkinTestAnswer>? SkinTestAnswer { get; set; } // 1 skin test co nhieu skin test answer
	}
}
