namespace Skincare_Product_Sales_System.Models
{
    public class SkinQuestionModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string SkinQuestionStatus { get; set; }
    }

    public class CreateSkinQuestionModel
    {
        public string QuestionText { get; set; }
    }

    public class UpdateSkinQuestionModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
    }
}
