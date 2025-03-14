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

        public async Task<List<SkinAnswer>> GetAllSkinAnswer()
        {
            var listSkinAnswer = await _unitOfWork.Repository<SkinAnswer>()
                .GetAll()
                .Include(sa => sa.SkinType)
                .Include(sa => sa.SkinQuestion)
                .AsNoTracking() //Chỉ đọc
                .ToListAsync();
            return listSkinAnswer;
        }

        public async Task<SkinAnswer> GetSkinAnswerById(int id)
        {
            var skinAnswer = await _unitOfWork.Repository<SkinAnswer>()
                .GetAll()
                .Include(sa => sa.SkinType)
                .Include(sa => sa.SkinQuestion)
                .AsNoTracking()
                .FirstOrDefaultAsync(sa => sa.Id == id);
            return skinAnswer;
                
        }
        public async Task<List<SkinAnswer>> GetSkinAnswersBySkinTypeId(int skinTypeId)
        {
            var listSkinAnswer = await _unitOfWork.Repository<SkinAnswer>()
                .ListAsync(sa => sa.SkinTypeId == skinTypeId,
                           includeProperties: q => q.Include(sa => sa.SkinType)
                                                    .Include(sa => sa.SkinQuestion));
            return listSkinAnswer.ToList();
        }

        public async Task<List<SkinAnswer>> GetSkinAnswersBySkinQuestionId(int skinQuestionId)
        {
            var listSkinAnswer = await _unitOfWork.Repository<SkinAnswer>()
                .ListAsync(sa => sa.QuestionId == skinQuestionId,
                           includeProperties: q => q.Include(sa => sa.SkinType)
                                                    .Include(sa => sa.SkinQuestion));
            return listSkinAnswer.ToList();
        }

        public async Task AddSkinAnswer(SkinAnswer skinAnswer)
        {
            skinAnswer.SkinAnswerStatus = SkinAnswerStatus.Active.ToString();
            await _unitOfWork.Repository<SkinAnswer>().AddAsync(skinAnswer);
            await _unitOfWork.Complete();
        }

        public async Task UpdateSkinAnswer(SkinAnswer skinAnswer)
        {
            skinAnswer.SkinAnswerStatus = SkinAnswerStatus.Active.ToString();
            _unitOfWork.Repository<SkinAnswer>().Update(skinAnswer);
            await _unitOfWork.Complete();
        }

        public async Task DeleteSkinAnswer(int id)
        {
            var skinAnswer = await _unitOfWork.Repository<SkinAnswer>().GetByIdAsync(id);
            if (skinAnswer != null)
            {
                skinAnswer.SkinAnswerStatus = SkinAnswerStatus.Inactive.ToString();
                _unitOfWork.Repository<SkinAnswer>().Update(skinAnswer);
                await _unitOfWork.Complete();
            }
        }
    }
}
