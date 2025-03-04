using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.OrderDetailService;
using Skincare_Product_Sales_System_Application.Services.SkinQuestionService;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;

namespace Skincare_Product_Sales_System.Controllers
{
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
                    return NotFound();
                return Ok(_mapper.Map<SkinQuestionModel>(skinQ));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("createSkinQuestion")]
        public async Task<IActionResult> CreateSkinQuestion(SkinQuestionModel skinQModel)
        {
            try
            {
                var skinQ = _mapper.Map<SkinQuestion>(skinQModel);

                await _skinQuestionService.AddSkinQuestionAsync(skinQ);
                return CreatedAtAction(nameof(GetSkinQuestionById), new { id = skinQModel.Id }, _mapper.Map<SkinQuestionModel>(skinQ));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("updateSkinQuestion")]
        public async Task<IActionResult> UpdateSkinQuestion([FromBody] SkinQuestionModel skinQModel)
        {
            try
            {
                var skinQ = _mapper.Map<SkinQuestion>(skinQModel);
                await _skinQuestionService.UpdateSkinQuestionAsync(skinQ);
                return Ok("SkinQuestion updated successfully.");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("deleteSkinQuestion/{id}")]
        public async Task<IActionResult> DeleteSkinQuestion(int id)
        {
            try
            {
                var skinQ = await _skinQuestionService.GetSkinQuestionByIdAsync(id);
                if (skinQ == null)
                {
                    return BadRequest("SkinQuestion not found");
                }
                await _skinQuestionService.DeleteSkinQuestionAsync(id);
                return Ok("SkinQuestion deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("listSkinQuestionActive")]
        public async Task<IActionResult> GetActiveSkinQuestion()
        {
            try
            {
                var skinQ = await _skinQuestionService.GetAllSkinQuestionAsync();
                var activeSkinQuestion = skinQ.Where(c => c.SkinQuestionStatus != SkinQuestionStatus.Inactive.ToString());
                var skinQModel = _mapper.Map<IEnumerable<SkinQuestionModel>>(activeSkinQuestion);
                return Ok(skinQModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("listSkinQuestionInactived")]
        public async Task<IActionResult> GetInactivedSkinQuestion()
        {
            try
            {
                var skinQ = await _skinQuestionService.GetAllSkinQuestionAsync();
                var activeSkinQuestion = skinQ.Where(c => c.SkinQuestionStatus == SkinQuestionStatus.Inactive.ToString());
                var skinQModel = _mapper.Map<IEnumerable<SkinQuestionModel>>(activeSkinQuestion);
                return Ok(skinQModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
