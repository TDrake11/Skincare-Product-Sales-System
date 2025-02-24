﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.ProductService;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;

namespace Skincare_Product_Sales_System.Controllers
{
	[Route("api/product")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly IMapper _mapper;
		public ProductController(IProductService productService, IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}
		[HttpGet("listProduct")]
		public async Task<IActionResult> ListProduct()
		{
			try
			{
				var products = await _productService.GetListProducts();
				var listProduct = _mapper.Map<List<ProductModel>>(products);
				return Ok(listProduct);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("getProductById/{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			try
			{
				var product = await _productService.GetProductById(id);
				return Ok(product);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("getListProductByName/{name}")]
		public async Task<IActionResult> GetListProductByName(string name)
		{
			try
			{
				var products = await _productService.GetListProductByName(name);
				return Ok(products);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("createProduct")]
		public async Task<IActionResult> CreateProduct([FromBody] ProductModel productModel)
		{
			try
			{
				var product = _mapper.Map<Product>(productModel);
				await _productService.CreateProduct(product);
				return Ok("Product created successfully");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("updateProduct")]
		public async Task<IActionResult> UpdateProduct([FromBody] ProductModel productModel)
		{
			try
			{
				var product = _mapper.Map<Product>(productModel);
				_productService.UpdateProduct(product);
				return Ok("Product updated successfully");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("deleteProduct/{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			try
			{
				var product = await _productService.GetProductById(id);
				if (product == null)
				{
					return BadRequest("Product not found");
				}
				product.ProductStatus = ProductStatus.Inactive.ToString();
				_productService.UpdateProduct(product);
				return Ok("Product deleted successfully");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
