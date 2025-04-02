using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetListSkinTypes()
        {
            try
            {
                var skinTypes = await _skinTypeService.GetListSkinTypesAsync();
                var listSkinTypes = _mapper.Map<List<SkinTypeModel>>(skinTypes);
                return Ok(listSkinTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
		[Authorize(Roles = "Admin")]
		[HttpPost("createSkinType")]
        public async Task<IActionResult> Create(CreateSkinTypeModel skinTypeModel)
        {
            try
            {
                var skinType = _mapper.Map<SkinType>(skinTypeModel);
                await _skinTypeService.AddSkinTypeAsync(skinType);
                return Ok("SkinType created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
		[Authorize(Roles = "Admin")]
		[HttpPut("updateSkinType")]
        public async Task<IActionResult> Update([FromBody] SkinTypeModel skinTypeModel)
        {
            try
            {
                string? normalizedStatus = skinTypeModel.SkinTypeStatus?.ToLower() switch
                {
                    "active" => "Active",
                    "inactive" => "Inactive",
                    _ => null
                };

                if (normalizedStatus == null)
                {
                    return BadRequest("The status is not valid.");
                }

                skinTypeModel.SkinTypeStatus = normalizedStatus;
                var skinType = _mapper.Map<SkinType>(skinTypeModel);
                await _skinTypeService.UpdateSkinTypeAsync(skinType);
                return Ok("SkinType updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
		[Authorize(Roles = "Admin")]
		[HttpDelete("deleteSkinType/{id}")]
        public async Task<IActionResult> DeleteSkinType(int id)
        {
            try
            {
                await _skinTypeService.DeleteSkinTypeAsync(id);
                return Ok("SkinType deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
