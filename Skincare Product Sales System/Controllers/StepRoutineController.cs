using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Application.Services.CommentService;
using Skincare_Product_Sales_System_Application.Services.StepRoutineServices;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;

namespace Skincare_Product_Sales_System.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class StepRoutineController : ControllerBase
    {
        private readonly IStepRoutineService _stepRoutineService;
        private readonly IMapper _mapper;

        public StepRoutineController(IStepRoutineService stepRoutineService, IMapper mapper)
        {
            _stepRoutineService = stepRoutineService;
            _mapper = mapper;
        }

		[HttpGet("listStepRoutines")]
        public async Task<IActionResult> GetAllStepRoutines()
        {
            try
            {
                var stepRoutines = await _stepRoutineService.GetAllStepRoutineAsync();
                var stepRoutineModel = _mapper.Map<IEnumerable<StepRoutineModel>>(stepRoutines);
                return Ok(stepRoutineModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		[HttpGet("getStepRoutine/{id}")]
        public async Task<IActionResult> GetStepRoutineById(int id)
        {
            try
            {
                var stepRoutine = await _stepRoutineService.GetStepRoutineByIdAsync(id);
                if (stepRoutine == null)
                {
                    return Ok("No StepRoutine found.");
                }
                    
                return Ok(_mapper.Map<StepRoutineModel>(stepRoutine));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getStepRoutineByRoutineId/{routineId}")]
        public async Task<IActionResult> GetStepRoutinesByRoutineId(int routineId)
        {
            try
            {
                var stepRoutine = await _stepRoutineService.GetStepRoutinesByRoutineIdAsync(routineId);
                if (stepRoutine == null || !stepRoutine.Any())
                {
                    return Ok("No StepRoutine found for this SkinCareRoutine.");
                }
                var stepRoutineModel = _mapper.Map<IEnumerable<StepRoutineModel>>(stepRoutine);
                return Ok(stepRoutineModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
		[Authorize(Roles = "Admin")]
		[HttpPost("createStepRoutine")]
        public async Task<IActionResult> CreateStepRoutine(CreateStepRoutineModel stepRoutineModel)
        {
            try
            {
                var stepRoutine = _mapper.Map<StepRoutine>(stepRoutineModel);
                await _stepRoutineService.AddStepRoutineAsync(stepRoutine);
                return Ok("StepRoutine created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
		[Authorize(Roles = "Admin")]
		[HttpPut("updateStepRoutine")]
        public async Task<IActionResult> UpdateStepRoutine([FromBody] UpdateStepRoutineModel stepRoutineModel)
        {
            try
            {
                string? normalizedStatus = stepRoutineModel.Status?.ToLower() switch
                {
                    "active" => "Active",
                    "inactive" => "Inactive",
                    _ => null
                };

                if (normalizedStatus == null)
                {
                    return BadRequest("The status is not valid.");
                }

                stepRoutineModel.Status = normalizedStatus;
                var stepRoutine = _mapper.Map<StepRoutine>(stepRoutineModel);
                await _stepRoutineService.UpdateStepRoutineAsync(stepRoutine);
                return Ok("StepRoutine updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
		[Authorize(Roles = "Admin")]
		[HttpDelete("deleteStepRoutine/{id}")]
        public async Task<IActionResult> DeleteStepRoutine(int id)
        {
            try
            {
                await _stepRoutineService.DeleteStepRoutineAsync(id);
                return Ok("StepRoutine deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
