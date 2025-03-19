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
        Task<List<StepRoutine>> GetAllStepRoutine();
        Task<StepRoutine> GetStepRoutineById(int id);
        Task<List<StepRoutine>> GetStepRoutinesByRoutineId(int routineId);
        Task AddStepRoutine(StepRoutine stepRoutine);
        Task UpdateStepRoutine(StepRoutine stepRoutine);
        Task DeleteStepRoutine(int id);
    }
}
