namespace Skincare_Product_Sales_System.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CommentStatus { get; set; }
        public int ProductId { get; set; }
        public string CustomerId { get; set; }
    }

    public class CreateCommentModel
    {
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ProductId { get; set; }
        public string CustomerId { get; set; }
    }

    public class UpdateCommentModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ProductId { get; set; }
        public string CustomerId { get; set; }
    }
}
