using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Entities
{
	public class SkinAnswer : BaseEntity
	{
		public string AnswerText { get; set; }

		[ForeignKey("SkinQuestion")]	
		public int? QuestionId { get; set; }
		public virtual SkinQuestion? SkinQuestion { get; set; } // 1 answer chi thuoc ve 1 question

		[ForeignKey("SkinType")]
		public int? SkinTypeId { get; set; }
		public virtual SkinType? SkinType { get; set; } // 1 answer chi thuoc ve 1 skin type

		//public virtual ICollection<SkinTestAnswer>? SkinTestAnswers { get; set; } // 1 answer co nhieu skin test answer
	}
}
