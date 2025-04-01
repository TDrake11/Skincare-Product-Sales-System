using Microsoft.EntityFrameworkCore;
using Skincare_Product_Sales_System_Application.Services.StepRoutineServices;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Service.StepRoutineService
{
    public class StepRoutineService : IStepRoutineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StepRoutineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StepRoutine>> GetAllStepRoutine()
        {
            var listStepRoutine = await _unitOfWork.Repository<StepRoutine>()
                .GetAll()
                .Include(sr => sr.Routine)
                .AsNoTracking() //Chỉ đọc
                .ToListAsync();
            return listStepRoutine;
        }

        public async Task<StepRoutine> GetStepRoutineById(int id)
        {
            var stepRoutine = await _unitOfWork.Repository<StepRoutine>()
                .GetAll()
                .Include(sr => sr.Routine)
                .AsNoTracking() //Chỉ đọc
                .FirstOrDefaultAsync(sr => sr.Id == id);
            return stepRoutine;

        }
        public async Task<List<StepRoutine>> GetStepRoutinesByRoutineId(int routineId)
        {
            var listStepRoutine = await _unitOfWork.Repository<StepRoutine>()
                .ListAsync(sr => sr.RoutineId == routineId, includeProperties: q => q.Include(sr => sr.Routine));
            return listStepRoutine.ToList();
        }

        public async Task AddStepRoutine(StepRoutine stepRoutine)
        {
            //Dùng GenericRepository để kiểm tra StepNumber trùng với trạng thái Active
               var existingActiveStep = await _unitOfWork.Repository<StepRoutine>()
               .ListAsync(
                   filter: s => s.RoutineId == stepRoutine.RoutineId
                                && s.StepNumber == stepRoutine.StepNumber
                                && s.Status == StepRoutineStatus.Active.ToString(),
                   orderBy: null,
                   includeProperties: null
               );

            if (existingActiveStep.Any())
            {
                throw new InvalidOperationException($"StepNumber {stepRoutine.StepNumber} has existed in RoutineId {stepRoutine.RoutineId}");
            }

            // Thêm StepRoutine mới
            stepRoutine.Status = StepRoutineStatus.Active.ToString();
            await _unitOfWork.Repository<StepRoutine>().AddAsync(stepRoutine);
            await _unitOfWork.Complete();

            // Kêu TotalSteps trong SkinCareRoutine (cộng thêm 1 thay vì truy vấn toàn bộ)
            var routine = await _unitOfWork.Repository<SkinCareRoutine>().GetByIdAsync((int)stepRoutine.RoutineId);
            if (routine != null)
            {
                routine.TotalSteps += 1;  // Cộng trực tiếp
                _unitOfWork.Repository<SkinCareRoutine>().Update(routine);
                await _unitOfWork.Complete();
            }
        }

        public async Task UpdateStepRoutine(StepRoutine stepRoutine)
        {
            var existingStepRoutine = await _unitOfWork.Repository<StepRoutine>().GetByIdAsync(stepRoutine.Id);

            if (existingStepRoutine == null)
            {
                throw new Exception("StepRoutine not found");
            }

            //if (existingStepRoutine.Status == StepRoutineStatus.Inactive.ToString())
            //{
            //    throw new Exception("This StepRoutine does not exist!!!!!!");
            //}

            if (existingStepRoutine.Status == StepRoutineStatus.Active.ToString() || existingStepRoutine.Status == StepRoutineStatus.Inactive.ToString())
            {
                var existingStep = await _unitOfWork.Repository<StepRoutine>()
                    .ListAsync(
                        filter: s => s.RoutineId == stepRoutine.RoutineId
                                     && s.StepNumber == stepRoutine.StepNumber
                                     && s.Id != stepRoutine.Id // Đảm bảo không so với chính nó
                                     && s.Status == StepRoutineStatus.Active.ToString(),
                        orderBy: null,
                        includeProperties: null
                    );

                if (existingStep.Any())
                {
                    throw new InvalidOperationException($"StepNumber {stepRoutine.StepNumber} has existed in RoutineId {stepRoutine.RoutineId}");
                }
            }

            existingStepRoutine.StepNumber = stepRoutine.StepNumber;
            existingStepRoutine.StepDescription = stepRoutine.StepDescription;
            existingStepRoutine.RoutineId = stepRoutine.RoutineId;
            existingStepRoutine.ProductId = stepRoutine.ProductId;

            _unitOfWork.Repository<StepRoutine>().Update(existingStepRoutine);
            await _unitOfWork.Complete();
        }

        public async Task DeleteStepRoutine(int id)
        {
            var stepRoutine = await _unitOfWork.Repository<StepRoutine>().GetByIdAsync(id);
            if (stepRoutine != null)
            {
                stepRoutine.Status = StepRoutineStatus.Inactive.ToString();
                _unitOfWork.Repository<StepRoutine>().Update(stepRoutine);
                await _unitOfWork.Complete();
            }

            var routine = await _unitOfWork.Repository<SkinCareRoutine>().GetByIdAsync((int)stepRoutine.RoutineId);
            if (routine != null)
            {
                routine.TotalSteps -= 1;  // trừ trực tiếp
                _unitOfWork.Repository<SkinCareRoutine>().Update(routine);
                await _unitOfWork.Complete();
            }
        }
    }
}
