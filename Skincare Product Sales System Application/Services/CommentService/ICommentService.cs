using Skincare_Product_Sales_System_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.CommentService
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllComments();
        Task<Comment?> GetCommentById(int id);
        Task<List<Comment>> GetCommentByProductId(int productId);
        Task<List<Comment>> GetCommentsByCustomerId(string customerId);
        Task AddCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(int id);
    }
}
