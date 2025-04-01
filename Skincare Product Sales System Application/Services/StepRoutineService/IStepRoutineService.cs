using Skincare_Product_Sales_System_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.StepRoutineServices
{
    public interface IStepRoutineService
    {
        Task<List<StepRoutine>> GetAllStepRoutineAsync();
        Task<StepRoutine?> GetStepRoutineByIdAsync(int id);
        Task<List<StepRoutine>> GetStepRoutinesByRoutineIdAsync(int routineId);
        Task AddStepRoutineAsync(StepRoutine stepRoutine);
        Task UpdateStepRoutineAsync(StepRoutine stepRoutine);
        Task DeleteStepRoutineAsync(int id);
    }
}
