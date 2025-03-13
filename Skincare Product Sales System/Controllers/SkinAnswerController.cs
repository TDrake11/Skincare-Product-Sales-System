using AutoMapper;
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
                var skinAs = await _skinAnswerService.GetAllSkinAnswer();
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
                var skinA = await _skinAnswerService.GetSkinAnswerById(id);
                if (skinA == null)
                    return NotFound();
                return Ok(_mapper.Map<SkinAnswerModel>(skinA));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("getSkinAnswersBySkinTypeId/{skinTypeId}")]
        public async Task<IActionResult> GetSkinAnswersBySkinTypeId(int skinTypeId)
        {
            try
            {
                var skinAnswers = await _skinAnswerService.GetSkinAnswersBySkinTypeId(skinTypeId);
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
                var skinAnswers = await _skinAnswerService.GetSkinAnswersBySkinQuestionId(skinQuestionId);
                var skinAnswerModels = _mapper.Map<IEnumerable<SkinAnswerModel>>(skinAnswers);
                return Ok(skinAnswerModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createSkinAnswer")]
        public async Task<IActionResult> CreateSkinAnswer(SkinAnswerModel skinAModel)
        {
            try
            {
                var skinA = _mapper.Map<SkinAnswer>(skinAModel);

                await _skinAnswerService.AddSkinAnswer(skinA);
                return CreatedAtAction(nameof(GetSkinAnswerById), new { id = skinAModel.Id }, _mapper.Map<SkinAnswerModel>(skinA));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("updateSkinAnswer")]
        public async Task<IActionResult> UpdateSkinAnswer([FromBody] SkinAnswerModel skinAModel)
        {
            try
            {
                var skinA = _mapper.Map<SkinAnswer>(skinAModel);
                await _skinAnswerService.UpdateSkinAnswer(skinA);
                return Ok("SkinAnswer updated successfully.");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("deleteSkinAnswer/{id}")]
        public async Task<IActionResult> DeleteSkinAnswer(int id)
        {
            try
            {
                var skinA = await _skinAnswerService.GetSkinAnswerById(id);
                if (skinA == null)
                {
                    return BadRequest("SkinAnswer not found");
                }
                await _skinAnswerService.DeleteSkinAnswer(id);
                return Ok("SkinAnswer deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("listSkinAnswerActive")]
        public async Task<IActionResult> GetActiveSkinAnswers()
        {
            try
            {
                var skinA = await _skinAnswerService.GetAllSkinAnswer();
                var activeSkinAnswers = skinA.Where(c => c.SkinAnswerStatus != SkinAnswerStatus.Inactive.ToString());
                var skinAModel = _mapper.Map<IEnumerable<SkinAnswerModel>>(activeSkinAnswers);
                return Ok(skinAModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("listSkinAnswerInactived")]
        public async Task<IActionResult> GetInactivedSkinAnswers()
        {
            try
            {
                var skinA = await _skinAnswerService.GetAllSkinAnswer();
                var activeSkinAnswers = skinA.Where(c => c.SkinAnswerStatus == SkinAnswerStatus.Inactive.ToString());
                var skinAModel = _mapper.Map<IEnumerable<SkinAnswerModel>>(activeSkinAnswers);
                return Ok(skinAModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
