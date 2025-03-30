using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<SkinType>> GetListSkinTypesAsync()
        {
            return await _unitOfWork.Repository<SkinType>().ListAllAsync();
        }

        public async Task AddSkinTypeAsync(SkinType skinType)
        {
            skinType.SkinTypeStatus = SkinTypeStatus.Active.ToString();
            await _unitOfWork.Repository<SkinType>().AddAsync(skinType);
            await _unitOfWork.Complete();
        }

        public async Task UpdateSkinTypeAsync(SkinType skinType)
        {
            _unitOfWork.Repository<SkinType>().Update(skinType);
            await _unitOfWork.Complete();
        }

        public async Task DeleteSkinTypeAsync(int id)
        {
            var skinType = await _unitOfWork.Repository<SkinType>().GetByIdAsync(id);
            if (skinType != null)
            {
                skinType.SkinTypeStatus = SkinTypeStatus.Inactive.ToString();
                _unitOfWork.Repository<SkinType>().Update(skinType);
                await _unitOfWork.Complete();
            }
            else
            {
                throw new Exception("SkinType not found");
            }
        }
    }
}
