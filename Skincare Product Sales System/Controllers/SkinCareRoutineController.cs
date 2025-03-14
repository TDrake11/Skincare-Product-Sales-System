using AutoMapper;
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
                var skinRTs = await _skinCareRoutineService.GetAllSkinCareRoutines(); 
                var skinRTModel = _mapper.Map<IEnumerable<SkinCareRoutineModel>>(skinRTs);
                return Ok(skinRTModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getSkinCareRoutinById/{id}")]
        public async Task<IActionResult> GetSkinCareRoutineById(int id)
        {
            try
            {
                var skinRT = await _skinCareRoutineService.GetSkinCareRoutineById(id);
                if (skinRT == null)
                    return NotFound();
                return Ok(_mapper.Map<SkinCareRoutineModel>(skinRT));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getSkinCareRoutinsBySkinTypeId/{skinTypeId}")]
        public async Task<IActionResult> GetSkinCareRoutineBySkinTypeId(int skinTypeId)
        {
            try
            {
                var skinRTs = await _skinCareRoutineService.GetSkinCareRoutineBySkinTypeId(skinTypeId);
                if (skinRTs == null || !skinRTs.Any())
                {
                    return NotFound("No SkinCareRoutine found for this product.");
                }
                var skinRTModel = _mapper.Map<IEnumerable<SkinCareRoutineModel>>(skinRTs);
                return Ok(skinRTModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createSkinCareRoutin")]
        public async Task<IActionResult> Create(CreateSkinCareRoutineModel skinRTModel)
        {
            try
            {
                var skinRT = _mapper.Map<SkinCareRoutine>(skinRTModel);

                await _skinCareRoutineService.AddSkinCareRoutineAsync(skinRT);
                return CreatedAtAction(nameof(GetSkinCareRoutineById), new { id = skinRT.Id }, _mapper.Map<SkinCareRoutineModel>(skinRT));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateSkinCareRoutin")]
        public async Task<IActionResult> Update([FromBody] UpdateSkinCareRoutineModel skinRTModel)
        {
            try
            {
                var skinRT = _mapper.Map<SkinCareRoutine>(skinRTModel);
                await _skinCareRoutineService.UpdateSkinCareRoutineAsync(skinRT);
                return Ok("SkinCareRoutine updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteSkinCareRoutin/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var skinRT = await _skinCareRoutineService.GetSkinCareRoutineById(id);
                if (skinRT == null)
                {
                    return BadRequest("SkinCareRoutine not found");
                }
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
