using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.OrderDetailService;
using Skincare_Product_Sales_System_Application.Services.SkinQuestionService;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;

namespace Skincare_Product_Sales_System.Controllers
{
    [Authorize(Roles = "Admin,Customer")]
	[Route("api/[controller]")]
    [ApiController]
    public class SkinQuestionController : ControllerBase
    {
        private readonly ISkinQuestionService _skinQuestionService;
        private readonly IMapper _mapper;

        public SkinQuestionController(ISkinQuestionService skinQuestionService, IMapper mapper)
        {
            _skinQuestionService = skinQuestionService;
            _mapper = mapper;
        }
        [HttpGet("ListSkinQuestions")]
        public async Task<IActionResult> GetAllSkinQuestions()
        {
            try
            {
                var skinQs = await _skinQuestionService.GetAllSkinQuestionAsync();
                var skinQModel = _mapper.Map<IEnumerable<SkinQuestionModel>>(skinQs);
                return Ok(skinQModel);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("getSkinQuestionById/{id}")]
        public async Task<IActionResult> GetSkinQuestionById(int id)
        {
            try
            {
                var skinQ = await _skinQuestionService.GetSkinQuestionByIdAsync(id);
                if (skinQ == null)
                {
                    return Ok("No SkinQuestion found.");
                }

                return Ok(_mapper.Map<SkinQuestionModel>(skinQ));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("createSkinQuestion")]
        public async Task<IActionResult> CreateSkinQuestion(CreateSkinQuestionModel skinQModel)
        {
            try
            {
                var skinQ = _mapper.Map<SkinQuestion>(skinQModel);
                await _skinQuestionService.AddSkinQuestionAsync(skinQ);
                return Ok("Category created successfully");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("updateSkinQuestion")]
        public async Task<IActionResult> UpdateSkinQuestion([FromBody] UpdateSkinQuestionModel skinQModel)
        {
            try
            {
                string? normalizedStatus = skinQModel.SkinQuestionStatus?.ToLower() switch
                {
                    "active" => "Active",
                    "inactive" => "Inactive",
                    _ => null
                };

                if (normalizedStatus == null)
                {
                    return BadRequest("The status is not valid.");
                }

                skinQModel.SkinQuestionStatus = normalizedStatus;

                var skinQ = _mapper.Map<SkinQuestion>(skinQModel);
                await _skinQuestionService.UpdateSkinQuestionAsync(skinQ);
                return Ok("SkinQuestion updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteSkinQuestion/{id}")]
        public async Task<IActionResult> DeleteSkinQuestion(int id)
        {
            try
            {
                await _skinQuestionService.DeleteSkinQuestionAsync(id);
                return Ok("SkinQuestion deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
