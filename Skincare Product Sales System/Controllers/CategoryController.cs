using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System_Application.DTOs;
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
        public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest(new { message = "Category data is required." });
            }

            // Kiểm tra xem CategoryStatus có hợp lệ không
            if (!Enum.IsDefined(typeof(CategoryStatus), categoryDto.CategoryStatus))
            {
                return BadRequest(new { message = "Invalid CategoryStatus. Accepted values: 1 (AVAILABEL), 2 (OUT_OF_STOCK)." });
            }

            // Tạo entity từ DTO (Không lấy ID từ DTO)
            var category = new Category
            {
                CategoryName = categoryDto.CategoryName,
                CategoryStatus = categoryDto.CategoryStatus
            };

            // Lưu vào database
            await _categoryRepository.Add(category);

            // Tạo DTO để trả về cho client (Bây giờ ID có giá trị từ database)
            var responseDto = new CategoryDTO
            {
                ID = category.Id, // Trả về ID do database tạo
                CategoryName = category.CategoryName,
                CategoryStatus = category.CategoryStatus
            };

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, responseDto);
        }


        // PUT: api/category/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest(new { message = "Category data is required." });
            }

            var existingCategory = await _categoryRepository.GetById(id);
            if (existingCategory == null)
            {
                return NotFound(new { message = $"Category with ID {id} not found." });
            }

            // Kiểm tra nếu CategoryStatus không hợp lệ
            if (!Enum.IsDefined(typeof(CategoryStatus), categoryDto.CategoryStatus))
            {
                return BadRequest(new { message = "Invalid category status value." });
            }

            // Cập nhật dữ liệu từ DTO vào Entity
            existingCategory.CategoryName = categoryDto.CategoryName;
            existingCategory.CategoryStatus = categoryDto.CategoryStatus;

            await _categoryRepository.Update(existingCategory);

            // Trả về DTO sau khi cập nhật
            var updatedCategoryDto = new CategoryDTO
            {
                ID = existingCategory.Id,
                CategoryName = existingCategory.CategoryName,
                CategoryStatus = existingCategory.CategoryStatus
            };

            return Ok(updatedCategoryDto); // Trả về thông tin đã cập nhật thay vì NoContent
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
