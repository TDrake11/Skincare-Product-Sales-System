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

        public async Task<List<Comment>> GetAllComments()
        {
            var comments = await _unitOfWork.Repository<Comment>()
                .GetAll()
                .Include(c => c.Product)
                .Include(c => c.Customer)
                .AsNoTracking()
                .ToListAsync();
            return comments;
        }

        public async Task<Comment?> GetCommentById(int id)
        {
            var comments = await _unitOfWork.Repository<Comment>()
                .GetAll()
                .Include(c => c.Product)
                .Include(c => c.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            return comments;
        }

        public async Task<List<Comment>> GetCommentByProductId(int productId)
        {
            var comments = await _unitOfWork.Repository<Comment>()
                .ListAsync(c => c.ProductId == productId,
                           includeProperties: q => q.Include(c => c.Product)
                                                    .Include(c => c.Customer));
            return comments.ToList();
        }

        public async Task<List<Comment>> GetCommentsByCustomerId(string customerId)
        {
            var comments = await _unitOfWork.Repository<Comment>()
                .ListAsync(c => c.CustomerId == customerId,
                           includeProperties: q => q.Include(c => c.Product)
                                                    .Include(c => c.Customer));
            return comments.ToList();
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
        }
    }
}
