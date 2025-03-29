using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.SkinCareRoutineService;
using Skincare_Product_Sales_System_Application.Services.SkinTestService;
using Skincare_Product_Sales_System_Domain.Entities;

namespace Skincare_Product_Sales_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinTestController : ControllerBase
    {
        private readonly ISkinTestService _skinTestService;
        private readonly IMapper _mapper;

        public SkinTestController(ISkinTestService skinTestService, IMapper mapper)
        {
            _skinTestService = skinTestService;
            _mapper = mapper;
        }

        [HttpGet("listSkinTestServices")]
        public async Task<IActionResult> GetAllSkinTestServices()
        {
            try
            {
                var skinTest = await _skinTestService.GetListSkinTests();
                var skinTModel = _mapper.Map<IEnumerable<SkinTestModel>>(skinTest);
                return Ok(skinTModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getSkinTestById/{id}")]
        public async Task<IActionResult> GetSkinTestById(int id)
        {
            try
            {
                var skinTest = await _skinTestService.GetSkinTestById(id);
                if (skinTest == null)
                {
                    return Ok("Skin test not found");
                }
                var skinTestModel = _mapper.Map<SkinTestModel>(skinTest);
                return Ok(skinTestModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createSkinTest")]
        public async Task<IActionResult> CreateSKinTest(CreateSkinTestModel skinTModel)
        {
            try
            {
                var skinT = _mapper.Map<SkinTest>(skinTModel);

                await _skinTestService.AddSkinTest(skinT);
                return CreatedAtAction(nameof(GetSkinTestById), new { id = skinT.Id }, _mapper.Map<SkinTestModel>(skinT));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
