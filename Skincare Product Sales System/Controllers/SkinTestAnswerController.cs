using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.SkinTestAnswerService;
using Skincare_Product_Sales_System_Application.Services.SkinTypeService;

namespace Skincare_Product_Sales_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinTestAnswerController : ControllerBase
    {
        private readonly ISkinTestAnswerService _skinTestAnswer;
        private readonly IMapper _mapper;

        public SkinTestAnswerController(ISkinTestAnswerService skinTestAnswerService, IMapper mapper)
        {
            _skinTestAnswer = skinTestAnswerService;
            _mapper = mapper;
        }

        [HttpGet("listSkinTestAnswer")]
        public async Task<IActionResult> GetListSkinTestAnswers()
        {
            try
            {
                var sta = await _skinTestAnswer.GetListSkinTestAnswers();
                var listSTA = _mapper.Map<List<SkinTestAnswerModel>>(sta);
                return Ok(listSTA);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getSkinTestAnswersBySkinTestId/{skinTestId}")]
        public async Task<IActionResult> GetListSkinTestsByCustomerId(int skinTestId)
        {
            try
            {
                var sta = await _skinTestAnswer.GetListSkinTestAnswersBySkinTestId(skinTestId);
                if (sta == null || !sta.Any())
                {
                    return Ok("No SkinTestAnswer found for this Customer.");
                }
                var skinTestAnswerModels = _mapper.Map<IEnumerable<SkinTestAnswerModel>>(sta);
                return Ok(skinTestAnswerModels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
