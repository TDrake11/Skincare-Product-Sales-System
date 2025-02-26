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

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _unitOfWork.Repository<Comment>().ListAllAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Comment>().GetByIdAsync(id);
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _unitOfWork.Repository<Comment>().AddAsync(comment);
            await _unitOfWork.Complete();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            _unitOfWork.Repository<Comment>().Update(comment);
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
