using Microsoft.EntityFrameworkCore;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.Repository<Product>().ListAsync(null, null, p => p.Include(c => c.Category).Include(s => s.SkinType).Include(st => st.Staff));
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var products = await _unitOfWork.Repository<Product>().ListAsync(
                filter: p => p.Id == id,
                includeProperties: query => query
                    .Include(p => p.Category)
                    .Include(p => p.Staff)
                    .Include(p => p.SkinType)
            );

            return products.FirstOrDefault();
        }

        public async Task AddProductAsync(Product product)
        {
            await _unitOfWork.Repository<Product>().AddAsync(product);
            await _unitOfWork.Complete();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _unitOfWork.Repository<Product>().Update(product);
            await _unitOfWork.Complete();
        }

        public async Task DeleteProductAsync(int id)
        {
            _unitOfWork.Repository<Product>().Delete(id);
            await _unitOfWork.Complete();
        }
    }
}
