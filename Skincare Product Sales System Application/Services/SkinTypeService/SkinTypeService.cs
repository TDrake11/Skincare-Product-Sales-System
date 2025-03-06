﻿using Microsoft.EntityFrameworkCore;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.SkinTypeService
{
    public class SkinTypeService : ISkinTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SkinTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SkinType>> GetListSkinTypes()
        {
            return await _unitOfWork.Repository<SkinType>()
                .GetAll()
                .Include(st => st.Products)
                .Include(st => st.SkinCareRoutines)
                .Include(st => st.SkinTests)
                .Include(st => st.SkinAnswer)
                .AsNoTracking() // Tối ưu cho truy vấn chỉ đọc
                .ToListAsync();
        }

        public async Task<SkinType?> GetSkinTypeById(int id)
        {
            return await _unitOfWork.Repository<SkinType>()
                .GetAll()
                .Include(st => st.Products)
                .Include(st => st.SkinCareRoutines)
                .Include(st => st.SkinTests)
                .Include(st => st.SkinAnswer)
                .AsNoTracking()
                .FirstOrDefaultAsync(st => st.Id == id);
        }

        public async Task AddSkinType(SkinType skinType)
        {
            await _unitOfWork.Repository<SkinType>().AddAsync(skinType);
            skinType.SkinTypeStatus = SkinTypeStatus.Active.ToString();
            await _unitOfWork.Complete();
        }

        public async Task UpdateSkinType(SkinType skinType)
        {
            _unitOfWork.Repository<SkinType>().Update(skinType);
            skinType.SkinTypeStatus = SkinTypeStatus.Active.ToString();
            await _unitOfWork.Complete();
        }

        public async Task DeleteSkinType(int id)
        {
            var skinType = await _unitOfWork.Repository<SkinType>().GetByIdAsync(id);
            if (skinType != null)
            {
                skinType.SkinTypeStatus = SkinTypeStatus.Inactive.ToString();
                _unitOfWork.Repository<SkinType>().Update(skinType);
                await _unitOfWork.Complete();
            }
        }
    }
}
