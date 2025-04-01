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

        public async Task<List<SkinQuestion>> GetAllSkinQuestionAsync()
        {
            var listSkinQuestion = (await _unitOfWork.Repository<SkinQuestion>().ListAllAsync()).ToList();
            return listSkinQuestion;
        }

        public async Task<SkinQuestion?> GetSkinQuestionByIdAsync(int id)
        {
            var skinQuestion = await _unitOfWork.Repository<SkinQuestion>().GetByIdAsync(id);
            return skinQuestion;
        }

        public async Task AddSkinQuestionAsync(SkinQuestion skinQuestion)
        {
            skinQuestion.SkinQuestionStatus = SkinQuestionStatus.Active.ToString();
            await _unitOfWork.Repository<SkinQuestion>().AddAsync(skinQuestion);
            await _unitOfWork.Complete();
        }

        public async Task UpdateSkinQuestionAsync(SkinQuestion skinQuestion)
        {
            _unitOfWork.Repository<SkinQuestion>().Update(skinQuestion);
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
            else
            {
                throw new Exception("SkinQuestion not found");
            }
        }
    }
}
