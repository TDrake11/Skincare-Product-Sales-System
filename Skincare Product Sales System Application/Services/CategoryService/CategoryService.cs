using Skincare_Product_Sales_System_Domain.Entities;
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

        public async Task AddCategoryAsync(Category category)
        {
            category.CategoryStatus = CategoryStatus.Active.ToString();
            await _unitOfWork.Repository<Category>().AddAsync(category);
            await _unitOfWork.Complete();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _unitOfWork.Repository<Category>().Update(category);
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
            else
            {
                throw new Exception("Category not found");
            }
        }
    }
}
