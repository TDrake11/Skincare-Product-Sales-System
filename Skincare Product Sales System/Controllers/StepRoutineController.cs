using AutoMapper;
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
                var stepRoutines = await _stepRoutineService.GetAllStepRoutine();
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
                var stepRoutine = await _stepRoutineService.GetStepRoutineById(id);
                if (stepRoutine == null)
                    return NotFound();
                return Ok(_mapper.Map<StepRoutineModel>(stepRoutine));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getActiveStepRoutineByRoutineId/{routineId}")]
        public async Task<IActionResult> GetActiveStepRoutinesByRoutineId(int routineId)
        {
            try
            {
                var stepRoutine = await _stepRoutineService.GetActiveStepRoutinesByRoutineId(routineId);
                if (stepRoutine == null || !stepRoutine.Any())
                {
                    return NotFound("No StepRoutine found for this SkinCareRoutine.");
                }
                var stepRoutineModel = _mapper.Map<IEnumerable<StepRoutineModel>>(stepRoutine);
                return Ok(stepRoutineModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getInactiveStepRoutineByRoutineId/{routineId}")]
        public async Task<IActionResult> GetInactiveStepRoutinesByRoutineId(int routineId)
        {
            try
            {
                var stepRoutine = await _stepRoutineService.GetInactiveStepRoutinesByRoutineId(routineId);
                if (stepRoutine == null || !stepRoutine.Any())
                {
                    return NotFound("No StepRoutine found for this SkinCareRoutine.");
                }
                var stepRoutineModel = _mapper.Map<IEnumerable<StepRoutineModel>>(stepRoutine);
                return Ok(stepRoutineModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createStepRoutine")]
        public async Task<IActionResult> CreateStepRoutine(CreateStepRoutineModel stepRoutineModel)
        {
            try
            {
                var stepRoutine = _mapper.Map<StepRoutine>(stepRoutineModel);

                await _stepRoutineService.AddStepRoutine(stepRoutine);
                return CreatedAtAction(nameof(GetStepRoutineById), new { id = stepRoutine.Id }, _mapper.Map<StepRoutineModel>(stepRoutine));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateStepRoutine")]
        public async Task<IActionResult> UpdateStepRoutine([FromBody] UpdateStepRoutineModel stepRoutineModel)
        {
            try
            {
                var stepRoutine = _mapper.Map<StepRoutine>(stepRoutineModel);
                await _stepRoutineService.UpdateStepRoutine(stepRoutine);
                return Ok("StepRoutine updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteStepRoutine/{id}")]
        public async Task<IActionResult> DeleteStepRoutine(int id)
        {
            try
            {
                var stepRoutine = await _stepRoutineService.GetStepRoutineById(id);
                if (stepRoutine == null)
                {
                    return BadRequest("StepRoutine not found");
                }
                await _stepRoutineService.DeleteStepRoutine(id);
                return Ok("StepRoutine deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
