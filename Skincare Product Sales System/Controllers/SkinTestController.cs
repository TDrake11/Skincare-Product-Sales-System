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
                var skinTest = await _skinTestService.GetListSkinTestsAsync();
                var skinTModel = _mapper.Map<IEnumerable<SkinTestModel>>(skinTest);
                return Ok(skinTModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getSkinTestsByCustomerId/{customerId}")]
        public async Task<IActionResult> GetListSkinTestsByCustomerId(string customerId)
        {
            try
            {
                var skinTest = await _skinTestService.GetListSkinTestsByCustomerIdAsync(customerId);
                if (skinTest == null || !skinTest.Any())
                {
                    return Ok("No SkinTest found for this Customer.");
                }
                var skinTestModel = _mapper.Map<IEnumerable<SkinTestModel>>(skinTest);
                return Ok(skinTestModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createSkinTest")]
        public async Task<IActionResult> CreateSkinTest([FromBody] CreateSkinTestModel request)
        {
            if (request == null || string.IsNullOrEmpty(request.CustomerId) || request.AnswerIds == null || !request.AnswerIds.Any())
        {
                return BadRequest("Invalid request data.");
            }

            try
            {
                var newSkinTest = await _skinTestService.CreateSkinTestAsync(request.CustomerId, request.SkinTypeId, request.AnswerIds);

                return Ok("SkinTest created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
