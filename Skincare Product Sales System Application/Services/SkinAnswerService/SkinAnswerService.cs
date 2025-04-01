using Microsoft.EntityFrameworkCore;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.SkinAnswerService
{
    public class SkinAnswerService : ISkinAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SkinAnswerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SkinAnswer>> GetAllSkinAnswerAsync()
        {
            var listSkinAnswer = await _unitOfWork.Repository<SkinAnswer>()
                .GetAll()
                .Include(sa => sa.SkinType)
                .Include(sa => sa.SkinQuestion)
                .ToListAsync();
            return listSkinAnswer;
        }

        public async Task<SkinAnswer?> GetSkinAnswerByIdAsync(int id)
        {
            var skinAnswer = await _unitOfWork.Repository<SkinAnswer>()
                .GetAll()
                .Include(sa => sa.SkinType)
                .Include(sa => sa.SkinQuestion)
                .FirstOrDefaultAsync(sa => sa.Id == id);
            return skinAnswer;
                
        }
        public async Task<List<SkinAnswer>> GetSkinAnswersBySkinTypeIdAsync(int skinTypeId)
        {
            var skinAnswer = _unitOfWork.Repository<SkinAnswer>().GetAll()
                .Include(sa => sa.SkinType)
                .Include(sa => sa.SkinQuestion)
                .Where(c => c.SkinTypeId == skinTypeId);

            return await skinAnswer.ToListAsync();
        }

        public async Task<List<SkinAnswer>> GetSkinAnswersBySkinQuestionIdAsync(int skinQuestionId)
        {
            var skinAnswer = _unitOfWork.Repository<SkinAnswer>().GetAll()
                .Include(sa => sa.SkinType)
                .Include(sa => sa.SkinQuestion)
                .Where(c => c.QuestionId == skinQuestionId);

            return await skinAnswer.ToListAsync();
        }

        public async Task AddSkinAnswerAsync(SkinAnswer skinAnswer)
        {
            skinAnswer.SkinAnswerStatus = SkinAnswerStatus.Active.ToString();
            await _unitOfWork.Repository<SkinAnswer>().AddAsync(skinAnswer);
            await _unitOfWork.Complete();
        }

        public async Task UpdateSkinAnswerAsync(SkinAnswer skinAnswer)
        {
            _unitOfWork.Repository<SkinAnswer>().Update(skinAnswer);
            await _unitOfWork.Complete();
        }

        public async Task DeleteSkinAnswerAsync(int id)
        {
            var skinAnswer = await _unitOfWork.Repository<SkinAnswer>().GetByIdAsync(id);
            if (skinAnswer != null)
            {
                skinAnswer.SkinAnswerStatus = SkinAnswerStatus.Inactive.ToString();
                _unitOfWork.Repository<SkinAnswer>().Update(skinAnswer);
                await _unitOfWork.Complete();
            }
            else
            {
                throw new Exception("Comment not found");
            }
        }
    }
}
