using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Entities
{
	public class SkinTestAnswer : BaseEntity
	{
		[ForeignKey("SkinTest")]
		public int SkinTestId { get; set; }
		public virtual SkinTest SkinTest { get; set; }

		[ForeignKey("SkinQuestion")]
		public int? QuestionId { get; set; }
		public virtual SkinQuestion? SkinQuestion { get; set; }

		[ForeignKey("SkinAnswer")]
		public int AnswerId { get; set; }
		public virtual SkinAnswer SkinAnswer { get; set; } 
	}
}
