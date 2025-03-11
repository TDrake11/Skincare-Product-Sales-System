﻿using Skincare_Product_Sales_System_Domain.Entities;
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

        public async Task<IEnumerable<Comment>> GetCommentByProductIdAsync(int productId)
        {
            var comments = await _unitOfWork.Repository<Comment>().ListAllAsync();
            return comments.Where(c => c.ProductId == productId);
        }

        public async Task<IEnumerable<Comment>> GetActiveCommentsByCustomerIdAsync(string customerId)
        {
            var comments = await _unitOfWork.Repository<Comment>().ListAllAsync();
            return comments.Where(c => c.CustomerId == customerId && c.CommentStatus != CommentStatus.Inactive.ToString());
        }

        public async Task<IEnumerable<Comment>> GetInactiveCommentsByCustomerIdAsync(string customerId)
        {
            var comments = await _unitOfWork.Repository<Comment>().ListAllAsync();
            return comments.Where(c => c.CustomerId == customerId && c.CommentStatus == CommentStatus.Inactive.ToString());
        }

        public async Task<IEnumerable<Comment>> GetActiveCommentsAsync()
        {
            var comments = await _unitOfWork.Repository<Comment>().ListAllAsync();
            return comments.Where(c => c.CommentStatus != CommentStatus.Inactive.ToString());
        }

        public async Task<IEnumerable<Comment>> GetInactiveCommentsAsync()
        {
            var comments = await _unitOfWork.Repository<Comment>().ListAllAsync();
            return comments.Where(c => c.CommentStatus == CommentStatus.Inactive.ToString());
        }

        public async Task AddCommentAsync(Comment comment)
        {
            comment.CommentStatus = CommentStatus.Approved.ToString();
            await _unitOfWork.Repository<Comment>().AddAsync(comment);
            await _unitOfWork.Complete();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            var existingComment = await _unitOfWork.Repository<Comment>().GetByIdAsync(comment.Id);

            if (existingComment == null)
            {
                throw new Exception("Comment not found");
            }

            if (existingComment.CommentStatus == CommentStatus.Inactive.ToString())
            {
                throw new Exception("This Comment does not exist!!!!!!");
            }

            existingComment.CommentStatus = CommentStatus.Approved.ToString();
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
