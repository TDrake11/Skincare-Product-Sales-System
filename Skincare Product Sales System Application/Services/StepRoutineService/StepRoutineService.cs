﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<List<StepRoutine>> GetAllStepRoutineAsync()
        {
            var listStepRoutine = await _unitOfWork.Repository<StepRoutine>()
                .GetAll()
                .Include(sr => sr.Routine)
                .Include(sr => sr.Product)
                .ToListAsync();
            return listStepRoutine;
        }

        public async Task<StepRoutine?> GetStepRoutineByIdAsync(int id)
        {
            var stepRoutine = await _unitOfWork.Repository<StepRoutine>()
                .GetAll()
                .Include(sr => sr.Routine)
                .Include(sr => sr.Product)
                .FirstOrDefaultAsync(sr => sr.Id == id);
            return stepRoutine;

        }
        public async Task<List<StepRoutine>> GetStepRoutinesByRoutineIdAsync(int routineId)
        {
            var stepRoutines = _unitOfWork.Repository<StepRoutine>().GetAll()
                .Include(sr => sr.Routine)
                .Include(sr => sr.Product)
                .Where(c => c.RoutineId == routineId)
                .OrderBy(c => c.StepNumber);

            return await stepRoutines.ToListAsync();
        }

        public async Task AddStepRoutineAsync(StepRoutine stepRoutine)
        {
            if (stepRoutine.RoutineId == null)
            {
                throw new InvalidOperationException("RoutineId cannot be null");
            }

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

            stepRoutine.Status = StepRoutineStatus.Active.ToString();
            await _unitOfWork.Repository<StepRoutine>().AddAsync(stepRoutine);
            await _unitOfWork.Complete();
            
            var routine = await _unitOfWork.Repository<SkinCareRoutine>().GetByIdAsync((int)stepRoutine.RoutineId);
            if (routine != null)
            {
                routine.TotalSteps += 1;
                _unitOfWork.Repository<SkinCareRoutine>().Update(routine);
                await _unitOfWork.Complete();
            }
        }

        public async Task UpdateStepRoutineAsync(StepRoutine stepRoutine)
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
                                     && s.Id != stepRoutine.Id
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

        public async Task DeleteStepRoutineAsync(int id)
        {
            var stepRoutine = await _unitOfWork.Repository<StepRoutine>().GetByIdAsync(id);
            if (stepRoutine != null)
            {
                stepRoutine.Status = StepRoutineStatus.Inactive.ToString();
                _unitOfWork.Repository<StepRoutine>().Update(stepRoutine);
                await _unitOfWork.Complete();
            }
            else
            {
                throw new Exception("StepRoutine not found");
            }

            var routine = await _unitOfWork.Repository<SkinCareRoutine>().GetByIdAsync((int)stepRoutine.RoutineId);
            if (routine != null)
            {
                routine.TotalSteps -= 1;
                _unitOfWork.Repository<SkinCareRoutine>().Update(routine);
                await _unitOfWork.Complete();
            }
        }
    }
}
