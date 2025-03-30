using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.CategoryService;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;

namespace Skincare_Product_Sales_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("getCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                var categoryModels = _mapper.Map<IEnumerable<CategoryModel>>(categories);
                return Ok(categoryModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            try
            {
                var category = _mapper.Map<Category>(request);
                await _categoryService.AddCategoryAsync(category);
                return Ok("Category created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryModel updateCategory)
        {
            try
            {
                string? normalizedStatus = updateCategory.CategoryStatus.ToLower() switch
                {
                    "active" => "Active",
                    "inactive" => "Inactive",
                    _ => null
                };

                if (normalizedStatus == null)
                {
                    return BadRequest("The status is not valid.");
                }

                updateCategory.CategoryStatus = normalizedStatus;
                var category = _mapper.Map<Category>(updateCategory);
                await _categoryService.UpdateCategoryAsync(category);
                return Ok("Category updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return Ok("Category deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
