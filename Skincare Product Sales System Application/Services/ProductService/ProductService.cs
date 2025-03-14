﻿using Microsoft.EntityFrameworkCore;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
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
			await _unitOfWork.Complete();
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

		public Product GetProductById(int id)
		{
			var product = _unitOfWork.Repository<Product>()
				.GetAll()
				.Include(p => p.Category)
				.Include(p => p.SkinType)
				.Include(p => p.Staff)
				.FirstOrDefault(p => p.Id == id);
			return product;
		}

		public void UpdateProduct(Product product)
		{
			_unitOfWork.Repository<Product>().Update(product);
			_unitOfWork.Complete();
		}

		public void DeleteProduct(int id)
		{
			var product = GetProductById(id);

			if (product != null)
			{
				product.ProductStatus = ProductStatus.Inactive.ToString();
				_unitOfWork.Repository<Product>().Update(product);
				_unitOfWork.Complete();
			}
			else
			{
				throw new Exception("Product not found");
			}
		}
	}
}
