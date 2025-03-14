using Skincare_Product_Sales_System_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.SkinCareRoutineService
{
    public interface ISkinCareRoutineService
    {
        Task<List<SkinCareRoutine>> GetAllSkinCareRoutines();
        Task<SkinCareRoutine> GetSkinCareRoutineById(int id);
        Task<List<SkinCareRoutine>> GetSkinCareRoutineBySkinTypeId(int skinTypeId);
        Task AddSkinCareRoutineAsync(SkinCareRoutine skinCareRoutine);
        Task UpdateSkinCareRoutineAsync(SkinCareRoutine skinCareRoutine);
        Task DeleteSkinCareRoutineAsync(int id);
    }
}
