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
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment?> GetCommentByIdAsync(int id);
        Task<IEnumerable<Comment>> GetCommentByProductIdAsync(int productId);
        Task<IEnumerable<Comment>> GetActiveCommentsByCustomerIdAsync(string customerId);
        Task<IEnumerable<Comment>> GetInactiveCommentsByCustomerIdAsync(string customerId);
        Task<IEnumerable<Comment>> GetActiveCommentsAsync();
        Task<IEnumerable<Comment>> GetInactiveCommentsAsync();
        Task AddCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(int id);
    }
}
