using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            var comments = await _unitOfWork.Repository<Comment>()
                .GetAll()
                .Include(c => c.Product)
                .Include(c => c.Customer)
                .ToListAsync();
            return comments;
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            var comments = await _unitOfWork.Repository<Comment>()
                .GetAll()
                .Include(c => c.Product)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(c => c.Id == id);
            return comments;
        }

        public async Task<List<Comment>> GetCommentByProductIdAsync(int productId)
        {
            var comments = _unitOfWork.Repository<Comment>().GetAll()
                .Include(c => c.Product)
                .Include(c => c.Customer)
                .Where(c => c.ProductId == productId);

            return await comments.ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsByCustomerIdAsync(string customerId)
        {
            var comments = _unitOfWork.Repository<Comment>().GetAll()
                .Include(c => c.Product)
                .Include(c => c.Customer)
                .Where(c => c.CustomerId == customerId);

            return await comments.ToListAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            comment.CommentStatus = CommentStatus.Approved.ToString();
            await _unitOfWork.Repository<Comment>().AddAsync(comment);
            await _unitOfWork.Complete();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            _unitOfWork.Repository<Comment>().Update(comment);
            comment.CommentStatus = CommentStatus.Approved.ToString();
            await _unitOfWork.Complete();
        }

        public async Task DeleteCommentAsync(int id)
        {
            var comment = await _unitOfWork.Repository<Comment>().GetByIdAsync(id);
            if (comment != null)
            {
                comment.CommentStatus = CommentStatus.Inactive.ToString();
                _unitOfWork.Repository<Comment>().Update(comment);
                await _unitOfWork.Complete();
            }
            else
            {
                throw new Exception("Comment not found");
            }
        }
    }
}
