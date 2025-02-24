﻿using Microsoft.EntityFrameworkCore;
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
		public async Task CreateProduct(Product product)
		{
			await _unitOfWork.Repository<Product>().AddAsync(product);
		}

		public async Task<List<Product>> GetListProducts()
		{
			var listProduct = await _unitOfWork.Repository<Product>()
				.GetAll()
				.Include(p => p.Category)  // Đảm bảo Category được load
				.Include(p => p.SkinType)  // Đảm bảo SkinType được load
				.ToListAsync();
			return listProduct;
		}

		public Task<Product> GetProductById(int id)
		{
			var product = _unitOfWork.Repository<Product>().GetByIdAsync(id);
			return product;
		}

		public async Task<List<Product>> GetListProductByName(string name)
		{
			var products = await _unitOfWork.Repository<Product>().GetAll().Where(p => p.ProductName.Contains(name)).ToListAsync();
			return products;
		}

		public void UpdateProduct(Product product)
		{
			_unitOfWork.Repository<Product>().Update(product);
		}
	}
}
