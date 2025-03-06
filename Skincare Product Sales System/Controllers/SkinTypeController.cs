using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.CategoryService;
using Skincare_Product_Sales_System_Application.Services.SkinTypeService;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;

namespace Skincare_Product_Sales_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinTypeController : ControllerBase
    {
        private readonly ISkinTypeService _skinTypeService;
        private readonly IMapper _mapper;

        public SkinTypeController(ISkinTypeService skinTypeService, IMapper mapper)
        {
            _skinTypeService = skinTypeService;
            _mapper = mapper;
        }

        [HttpGet("listSkinType")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var skinType = await _skinTypeService.GetAllSkinTypeAsync();
                var skinTypeModel = _mapper.Map<IEnumerable<SkinTypeModel>>(skinType);
                return Ok(skinTypeModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getSkinTypeById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var skinType = await _skinTypeService.GetSkinTypeByIdAsync(id);
                if (skinType == null)
                    return NotFound();
                return Ok(_mapper.Map<SkinTypeModel>(skinType));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createSkinType")]
        public async Task<IActionResult> Create(SkinTypeModel skinTypeModel)
        {
            try
            {
                var SkinType = _mapper.Map<SkinType>(skinTypeModel);

                await _skinTypeService.AddSkinTypeAsync(SkinType);
                return CreatedAtAction(nameof(GetById), new { id = SkinType.Id }, _mapper.Map<SkinTypeModel>(SkinType));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateSkinType")]
        public async Task<IActionResult> Update([FromBody] SkinTypeModel skinTypeModel)
        {
            try
            {
                var skinType = _mapper.Map<SkinType>(skinTypeModel);
                await _skinTypeService.UpdateSkinTypeAsync(skinType);
                return Ok("SkinType updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteSkinType/{id}")]
        public async Task<IActionResult> DeleteSkinType(int id)
        {
            try
            {
                var skinType = await _skinTypeService.GetSkinTypeByIdAsync(id);
                if (skinType == null)
                {
                    return BadRequest("SkinType not found");
                }
                await _skinTypeService.DeleteSkinTypeAsync(id);
                return Ok("SkinType deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
