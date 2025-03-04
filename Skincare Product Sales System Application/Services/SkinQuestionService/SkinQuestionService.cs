using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.SkinQuestionService
{
    public class SkinQuestionService : ISkinQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SkinQuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SkinQuestion>> GetAllSkinQuestionAsync()
        {
            return await _unitOfWork.Repository<SkinQuestion>().ListAllAsync();
        }

        public async Task<SkinQuestion?> GetSkinQuestionByIdAsync(int id)
        {
            return await _unitOfWork.Repository<SkinQuestion>().GetByIdAsync(id);
        }

        public async Task AddSkinQuestionAsync(SkinQuestion skinQuestion)
        {
            await _unitOfWork.Repository<SkinQuestion>().AddAsync(skinQuestion);
            skinQuestion.SkinQuestionStatus = SkinQuestionStatus.Active.ToString(); // Gán trạng thái sau khi thêm mới
            await _unitOfWork.Complete();
        }

        public async Task UpdateSkinQuestionAsync(SkinQuestion skinQuestion)
        {
            _unitOfWork.Repository<SkinQuestion>().Update(skinQuestion);
            skinQuestion.SkinQuestionStatus = SkinQuestionStatus.Active.ToString();
            await _unitOfWork.Complete();
        }

        public async Task DeleteSkinQuestionAsync(int id)
        {
            var skinQuestion = await _unitOfWork.Repository<SkinQuestion>().GetByIdAsync(id);
            if (skinQuestion != null)
            {
                skinQuestion.SkinQuestionStatus = SkinQuestionStatus.Inactive.ToString();
                _unitOfWork.Repository<SkinQuestion>().Update(skinQuestion);
                await _unitOfWork.Complete();
            }
        }
    }
}
