using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.SkinAnswerService;
using Skincare_Product_Sales_System_Application.Services.SkinQuestionService;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;

namespace Skincare_Product_Sales_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinAnswerController : ControllerBase
    {
        private readonly ISkinAnswerService _skinAnswerService;
        private readonly IMapper _mapper;

        public SkinAnswerController(ISkinAnswerService skinAnswerService, IMapper mapper)
        {
            _skinAnswerService = skinAnswerService;
            _mapper = mapper;
        }
        [HttpGet("ListSkinAnswer")]
        public async Task<IActionResult> GetAllSkinAnswer()
        {
            try
            {
                var skinAs = await _skinAnswerService.GetAllSkinAnswerAsync();
                var skinAModel = _mapper.Map<IEnumerable<SkinAnswerModel>>(skinAs);
                return Ok(skinAModel);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
		[HttpGet("getSkinAnswerById/{id}")]
        public async Task<IActionResult> GetSkinAnswerById(int id)
        {
            try
            {
                var skinA = await _skinAnswerService.GetSkinAnswerByIdAsync(id);
                if (skinA == null)
                {
                    return Ok("No skin answer found.");
                }

                return Ok(_mapper.Map<SkinAnswerModel>(skinA));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		[HttpGet("getSkinAnswersBySkinTypeId/{skinTypeId}")]
        public async Task<IActionResult> GetSkinAnswersBySkinTypeId(int skinTypeId)
        {
            try
            {
                var skinAnswers = await _skinAnswerService.GetSkinAnswersBySkinTypeIdAsync(skinTypeId);
                if (skinAnswers == null || !skinAnswers.Any())
                {
                    return Ok("No SkinAnswers found for this SkinType.");
                }
                    
                var skinAnswerModels = _mapper.Map<IEnumerable<SkinAnswerModel>>(skinAnswers);
                return Ok(skinAnswerModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		[HttpGet("getSkinAnswersBySkinQuestionId/{skinQuestionId}")]
        public async Task<IActionResult> GetSkinAnswersBySkinQuestionId(int skinQuestionId)
        {
            try
            {
                var skinAnswers = await _skinAnswerService.GetSkinAnswersBySkinQuestionIdAsync(skinQuestionId);

                if (skinAnswers == null || !skinAnswers.Any())
                {
                    return Ok("No SkinAnswers found for this SkinQuestion.");
                }

                var skinAnswerModels = _mapper.Map<IEnumerable<SkinAnswerModel>>(skinAnswers);
                return Ok(skinAnswerModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
		[Authorize(Roles = "Admin")]
		[HttpPost("createSkinAnswer")]
        public async Task<IActionResult> CreateSkinAnswer(CreateSkinAnswerModel skinAModel)
        {
            try
            {
                var skinAnswer = _mapper.Map<SkinAnswer>(skinAModel);
                await _skinAnswerService.AddSkinAnswerAsync(skinAnswer);
                return Ok("SkinAnswer created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
		[Authorize(Roles = "Admin")]
		[HttpPut("updateSkinAnswer")]
        public async Task<IActionResult> UpdateSkinAnswer([FromBody] UpdateSkinAnswerModel skinAModel)
        {
            try
            {
                string? normalizedStatus = skinAModel.SkinAnswerStatus?.ToLower() switch
                {
                    "active" => "Active",
                    "inactive" => "Inactive",
                    _ => null
                };

                if (normalizedStatus == null)
                {
                    return BadRequest("The status is not valid.");
                }

                skinAModel.SkinAnswerStatus = normalizedStatus;
                var skinA = _mapper.Map<SkinAnswer>(skinAModel);
                await _skinAnswerService.UpdateSkinAnswerAsync(skinA);
                return Ok("SkinAnswer updated successfully.");
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message); 
            }
        }
		[Authorize(Roles = "Admin")]
		[HttpDelete("deleteSkinAnswer/{id}")]
        public async Task<IActionResult> DeleteSkinAnswer(int id)
        {
            try
            {
                await _skinAnswerService.DeleteSkinAnswerAsync(id);
                return Ok("SkinAnswer deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
