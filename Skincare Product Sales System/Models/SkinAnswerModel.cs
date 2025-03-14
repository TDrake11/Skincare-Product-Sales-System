namespace Skincare_Product_Sales_System.Models
{
    public class SkinAnswerModel
    {
        public int Id { get; set; }

        public string AnswerText { get; set; }
        public string SkinAnswerStatus { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int SkinTypeId { get; set; }
        public string SkinTypeName { get; set; }
    }

    public class CreateSkinAnswerModel
    {
        public string AnswerText { get; set; }
        public int QuestionId { get; set; }
        public int SkinTypeId { get; set; }
    }

    public class UpdateSkinAnswerModel
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public int QuestionId { get; set; }
        public int SkinTypeId { get; set; }
    }
}
