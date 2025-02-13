using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Entities
{
	public class SkinQuestion : BaseEntity
	{
		public string QuestionText { get; set; }

		public virtual ICollection<SkinAnswer>? SkinAnswers { get; set; } // 1 question co nhieu answer

		//public virtual ICollection<SkinTestAnswer>? SkinTestAnswers { get; set; } // 1 question co the co nhieu test answer
	}
}
