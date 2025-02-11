using Skincare_Product_Sales_System_Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Entities
{
	public class Comment : BaseEntity
	{
		public string Content { get; set; }
		public int Rating { get; set; } // 1-5
		public DateTime CreatedDate { get; set; }
		public CommentStatus CommentStatus { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public virtual Product Product { get; set; } // 1 comment chi co 1 product

		[ForeignKey("Customer")]
		public int CustomerId { get; set; }
		public virtual User Customer { get; set; } // 1 comment chi co 1 khach hang
	}
}
