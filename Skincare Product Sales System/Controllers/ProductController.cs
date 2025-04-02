using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.ProductService;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;

namespace Skincare_Product_Sales_System.Controllers
{
	//[Authorize(Roles ="Admin,Staff")]
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
				var baseUrl = $"{Request.Scheme}://{Request.Host}";

				// Cập nhật đường dẫn ảnh
				foreach (var product in listProduct)
				{
					if (!string.IsNullOrEmpty(product.Image))
					{
						product.Image = $"{baseUrl}{product.Image}";
					}
				}

				return Ok(listProduct);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpGet("getProductById/{id}")]
		public IActionResult GetProductById(int id)
		{
			try
			{
				var product = _productService.GetProductDeatilById(id);
				if (product == null)
				{
					return BadRequest("Product not found");
				}

				var productModel = _mapper.Map<ProductModel>(product);
				var baseUrl = $"{Request.Scheme}://{Request.Host}";

				if (!string.IsNullOrEmpty(productModel.Image))
				{
					productModel.Image = $"{baseUrl}{productModel.Image}";
				}

				return Ok(productModel);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpPost("createProduct")]
		public async Task<IActionResult> CreateProduct([FromForm] ProductModel productModel)
		{
			try
			{
				// Ánh xạ từ DTO sang Entity
				var product = _mapper.Map<Product>(productModel);
				product.ProductStatus = ProductStatus.Available.ToString();

				// Xử lý ảnh sản phẩm
				if (productModel.AttachmentFile != null && productModel.AttachmentFile.Length > 0)
				{
					var productFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Products", product.Id.ToString());

					if (!Directory.Exists(productFolder))
					{
						Directory.CreateDirectory(productFolder);
					}

					var fileName = Path.GetFileName(productModel.AttachmentFile.FileName);
					var filePath = Path.Combine(productFolder, fileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await productModel.AttachmentFile.CopyToAsync(stream);
					}

					// Lưu đường dẫn tương đối vào database
					product.Image = $"/Uploads/Products/{product.Id}/{fileName}";
				}

				await _productService.CreateProduct(product);
				return Ok(new { message = "Product created successfully", product });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpPut("updateProduct")]
		public async Task<IActionResult> UpdateProduct([FromForm] ProductUpdateModel productModel)
		{
			try
			{
				var product = _productService.GetProductById(productModel.Id);
				if (product == null)
				{
					return NotFound("Product not found");
				}


				// Xử lý cập nhật ảnh nếu có ảnh mới
				if (productModel.AttachmentFile != null && productModel.AttachmentFile.Length > 0)
				{
					// Cập nhật dữ liệu từ productModel vào product đã có
					_mapper.Map(productModel, product);
					var productFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Products", product.Id.ToString());

					if (!Directory.Exists(productFolder))
					{
						Directory.CreateDirectory(productFolder);
					}

					var fileName = Path.GetFileName(productModel.AttachmentFile.FileName);
					var filePath = Path.Combine(productFolder, fileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await productModel.AttachmentFile.CopyToAsync(stream);
					}

					// Cập nhật đường dẫn ảnh mới
					product.Image = $"/Uploads/Products/{product.Id}/{fileName}";
				}
				else
				{
					productModel.Image = product.Image;
					// Cập nhật dữ liệu từ productModel vào product đã có
					_mapper.Map(productModel, product);
				}
				await _productService.UpdateProduct(product);
				return Ok(new { message = "Product updated successfully", product });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpDelete("deleteProduct/{id}")]
		public  async Task<IActionResult> DeleteProduct(int id)
		{
			try
			{
				var product = _productService.GetProductById(id);
				await _productService.DeleteProduct(product);
				return Ok("Product deleted successfully");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
