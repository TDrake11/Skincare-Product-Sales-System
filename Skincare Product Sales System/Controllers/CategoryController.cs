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

        [HttpGet("listCategory")]
        public async Task<IActionResult> GetAll()
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

        [HttpGet("getCategoryById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var categorys = await _categoryService.GetCategoryByIdAsync(id);
                if (categorys == null)
                return NotFound();
                return Ok(_mapper.Map<CategoryModel>(categorys));
        }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createCategory")]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            try
            {
                var category = _mapper.Map<Category>(request);

                await _categoryService.AddCategoryAsync(category);
                return CreatedAtAction(nameof(GetById), new { id = category.Id }, _mapper.Map<CategoryModel>(category));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateCategory")]
        #region OldUpdateMethod
        // [HttpPut("updateCategory")]
        // public async Task<IActionResult> Update(int id, CategoryModel categoryModel)
        // {
        //     if (id != categoryModel.Id)
        //         return BadRequest();

        //     var category = _mapper.Map<Category>(categoryModel);
        //     await _categoryService.UpdateCategoryAsync(category);
        //     return NoContent();
        // }
        #endregion
        public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest updateCategory)
        {
            try
            {
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
        #region OldDeleteMethod
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _categoryService.DeleteCategoryAsync(id);
        //    return NoContent();
        //}
        #endregion
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
        {
                    return BadRequest("Category not found");
                }
            await _categoryService.DeleteCategoryAsync(id);
                return Ok("Category deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
