using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.CommentService;
using Skincare_Product_Sales_System_Application.Services.SkinCareRoutineService;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;

namespace Skincare_Product_Sales_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinCareRoutineController : ControllerBase
    {
        private readonly ISkinCareRoutineService _skinCareRoutineService;
        private readonly IMapper _mapper;

        public SkinCareRoutineController(ISkinCareRoutineService skinCareRoutineService, IMapper mapper)
        {
            _skinCareRoutineService = skinCareRoutineService;
            _mapper = mapper;
        }

        [HttpGet("listSkinCareRoutines")]
        public async Task<IActionResult> GetAllSkinCareRoutines()
        {
            try
            {
                var skinRTs = await _skinCareRoutineService.GetAllSkinCareRoutinesAsync(); 
                var skinRTModel = _mapper.Map<IEnumerable<SkinCareRoutineModel>>(skinRTs);
                return Ok(skinRTModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getSkinCareRoutineById/{id}")]
        public async Task<IActionResult> GetSkinCareRoutineById(int id)
        {
            try
            {
                var skinRT = await _skinCareRoutineService.GetSkinCareRoutineByIdAsync(id);

                if (skinRT == null)
                {
                    return Ok("No SkinCareRoutin found.");
                }

                return Ok(_mapper.Map<SkinCareRoutineModel>(skinRT));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getSkinCareRoutinesBySkinTypeId/{skinTypeId}")]
        public async Task<IActionResult> GetSkinCareRoutineBySkinTypeId(int skinTypeId)
        {
            try
            {
                var skinRTs = await _skinCareRoutineService.GetSkinCareRoutineBySkinTypeIdAsync(skinTypeId);
                if (skinRTs == null || !skinRTs.Any())
                {
                    return Ok("No SkinCareRoutine found for this SkinType.");
                }
                var skinRTModel = _mapper.Map<IEnumerable<SkinCareRoutineModel>>(skinRTs);
                return Ok(skinRTModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createSkinCareRoutine")]
        public async Task<IActionResult> CreateSkinCareRoutine(CreateSkinCareRoutineModel skinRTModel)
        {
            try
            {
                var skinRT = _mapper.Map<SkinCareRoutine>(skinRTModel);
                await _skinCareRoutineService.AddSkinCareRoutineAsync(skinRT);
                return Ok("SkinCareRoutine created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateSkinCareRoutine")]
        public async Task<IActionResult> UpdateSkinCareRoutine([FromBody] UpdateSkinCareRoutineModel skinRTModel)
        {
            try
            {
                string? normalizedStatus = skinRTModel.Status.ToLower() switch
                {
                    "active" => "Active",
                    "inactive" => "Inactive",
                    _ => null
                };

                if (normalizedStatus == null)
                {
                    return BadRequest("The status is not valid.");
                }

                skinRTModel.Status = normalizedStatus;
                var skinRT = _mapper.Map<SkinCareRoutine>(skinRTModel);
                await _skinCareRoutineService.UpdateSkinCareRoutineAsync(skinRT);
                return Ok("SkinCareRoutine updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteSkinCareRoutine/{id}")]
        public async Task<IActionResult> DeleteSkinCareRoutine(int id)
        {
            try
            {
                await _skinCareRoutineService.DeleteSkinCareRoutineAsync(id);
                return Ok("SkinCareRoutine deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
