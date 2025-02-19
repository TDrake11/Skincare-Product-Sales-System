using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System_Domain.DTO;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
using Skincare_Product_Sales_System_Domain.Interfaces;

namespace Skincare_Product_Sales_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryRepository.GetAll();
            return Ok(categories);
        }

        // GET: api/category/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: api/category
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Category data is required.");
            }

            // Chuyển đổi từ DTO sang Entity
            var category = new Category
            {
                CategoryName = categoryDto.CategoryName,
                CategoryStatus = (CategoryStatus)categoryDto.CategoryStatus
            };

            await _categoryRepository.Add(category);

            // Chuyển đổi từ Entity sang DTO để trả về
            var responseDto = new CategoryDTO
            {
                CategoryName = category.CategoryName,
                CategoryStatus = (int)category.CategoryStatus
            };

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, responseDto);
        }

        // PUT: api/category/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Category data is required.");
            }

            var existingCategory = await _categoryRepository.GetById(id);
            if (existingCategory == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            // Cập nhật dữ liệu từ DTO vào Entity
            existingCategory.CategoryName = categoryDto.CategoryName;
            existingCategory.CategoryStatus = (CategoryStatus)categoryDto.CategoryStatus;

            await _categoryRepository.Update(existingCategory);

            return NoContent(); // Trả về 204 (No Content) khi cập nhật thành công
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            await _categoryRepository.Delete(id);

            return NoContent(); // Trả về 204 (No Content) khi xóa thành công
        }

    }
}
