﻿using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.Repository<Category>().ListAllAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Category>().GetByIdAsync(id);
        }

        public async Task AddCategoryAsync(Category category) 
        {
            category.CategoryStatus = CategoryStatus.Active.ToString(); // Gán trước khi thêm vào DB
            await _unitOfWork.Repository<Category>().AddAsync(category);
            await _unitOfWork.Complete();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _unitOfWork.Repository<Category>().GetByIdAsync(category.Id);

            if (existingCategory == null)
            {
                throw new Exception("Category not found");
            }

            if (existingCategory.CategoryStatus == CategoryStatus.Inactive.ToString())
            {
                throw new Exception("This category does not exist!!!!!!");
            }

            existingCategory.CategoryName = category.CategoryName;
            existingCategory.CategoryStatus = CategoryStatus.Active.ToString();

            _unitOfWork.Repository<Category>().Update(existingCategory);
            await _unitOfWork.Complete();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if (category != null)
            {
                category.CategoryStatus = CategoryStatus.Inactive.ToString();
                _unitOfWork.Repository<Category>().Update(category);
            await _unitOfWork.Complete();
        }
    }
}
}
