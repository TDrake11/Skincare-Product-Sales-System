using Microsoft.EntityFrameworkCore;
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

        public async Task<List<SkinCareRoutine>> GetAllSkinCareRoutines()
        {
            var listSkinRoutine = await _unitOfWork.Repository<SkinCareRoutine>()
                .GetAll()
                .Include(sr => sr.SkinType)
                .AsNoTracking()
                .ToListAsync();
            return listSkinRoutine;
        }

        public async Task<SkinCareRoutine> GetSkinCareRoutineById(int id) 
        {
            var listSkinRoutine = await _unitOfWork.Repository<SkinCareRoutine>()
                .GetAll()
                .Include(sr => sr.SkinType)
                .AsNoTracking()
                .FirstOrDefaultAsync(sr => sr.Id == id);
            return listSkinRoutine;
        }

        public async Task<List<SkinCareRoutine>> GetSkinCareRoutineBySkinTypeId(int skinTypeId)
        {
            var listSkinRoutine = await _unitOfWork.Repository<SkinCareRoutine>()
                .ListAsync(sr => sr.SkinTypeId == skinTypeId, includeProperties: q => q.Include(sr => sr.SkinType));
            return listSkinRoutine.ToList();
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
