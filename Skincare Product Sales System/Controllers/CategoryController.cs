using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.CategoryService;
using Skincare_Product_Sales_System_Domain.Entities;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var categoryModels = _mapper.Map<IEnumerable<CategoryModel>>(categories);
            return Ok(categoryModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();
            return Ok(_mapper.Map<CategoryModel>(category));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = _mapper.Map<Category>(categoryModel);

            await _categoryService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, _mapper.Map<CategoryModel>(category));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryModel categoryModel)
        {
            if (id != categoryModel.Id)
                return BadRequest();

            var category = _mapper.Map<Category>(categoryModel);
            await _categoryService.UpdateCategoryAsync(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
