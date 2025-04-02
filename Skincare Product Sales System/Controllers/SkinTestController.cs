using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public SkinTestController(ISkinTestService skinTestService, IMapper mapper, UserManager<User> userManager)
        {
            _skinTestService = skinTestService;
            _mapper = mapper;
            _userManager = userManager;
		}
		[Authorize(Roles = "Admin")]
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
		[Authorize(Roles = "Admin,Customer")]
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
		[Authorize]
		[HttpPost("createSkinTest")]
        public async Task<IActionResult> CreateSkinTest(int skinTypeId)
        {
            try
            {
				var user = await _userManager.GetUserAsync(User);
				var skinTest = new SkinTest
				{
					CustomerId = user.Id,
					SkinTypeId = skinTypeId,
					CreateDate = DateTime.Now
				};
				await _skinTestService.AddSkinTestAsync(skinTest);
                return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}


		}

    }
}
