namespace Skincare_Product_Sales_System.Models
{
    public class SkinAnswerModel
    {
        public int Id { get; set; }

        public string AnswerText { get; set; }

        public int QuestionId { get; set; }

        public int SkinTypeId { get; set; }
    }
}
