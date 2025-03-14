using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.SkinCareRoutineService
{
    public class SkinCareRoutineService : ISkinCareRoutineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SkinCareRoutineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SkinCareRoutine>> GetAllSkinCareRoutinesAsync()
        {
            return await _unitOfWork.Repository<SkinCareRoutine>().ListAllAsync();
        }

        public async Task<SkinCareRoutine> GetSkinCareRoutineByIdAsync(int id) 
        {
            return await _unitOfWork.Repository<SkinCareRoutine>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<SkinCareRoutine>> GetSkinCareRoutineBySkinTypeIdAsync(int skinTypeId)
        {
            var skinCareRoutines = await _unitOfWork.Repository<SkinCareRoutine>().ListAllAsync();
            return skinCareRoutines.Where(c => c.SkinTypeId == skinTypeId);
        }

        public async Task AddSkinCareRoutineAsync(SkinCareRoutine skinCareRoutine)
        {
            await _unitOfWork.Repository<SkinCareRoutine>().AddAsync(skinCareRoutine);
            skinCareRoutine.Status = SkinCareRoutineStatus.Active.ToString();
            await _unitOfWork.Complete();
        }

        public async Task UpdateSkinCareRoutineAsync(SkinCareRoutine skinCareRoutine)
        {
            _unitOfWork.Repository<SkinCareRoutine>().Update(skinCareRoutine);
            skinCareRoutine.Status = SkinCareRoutineStatus.Active.ToString();
            await _unitOfWork.Complete();
        }

        public async Task DeleteSkinCareRoutineAsync(int id)
        {
            var skinCareRoutine = await _unitOfWork.Repository<SkinCareRoutine>().GetByIdAsync(id);
            if (skinCareRoutine != null)
            {
                skinCareRoutine.Status = SkinCareRoutineStatus.Inactive.ToString();
                _unitOfWork.Repository<SkinCareRoutine>().Update(skinCareRoutine);
                await _unitOfWork.Complete();
            }
        }
    }
}
