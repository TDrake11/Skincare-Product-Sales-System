namespace Skincare_Product_Sales_System.Models
{
    public class SkinTestAnswerModel
    {
        public int Id { get; set; }
        public int SkinTestId { get; set; }
        public int QuestionId { get; set; }
        public string? QuestionText { get; set; }
        public int AnswerId { get; set; }
        public string? AnswerText { get; set; }
    }
}
