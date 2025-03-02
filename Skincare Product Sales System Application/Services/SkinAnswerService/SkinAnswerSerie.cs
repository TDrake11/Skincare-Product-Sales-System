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
    public class SkinAnswerSerie : ISkinAnswerSerie
    {
        private readonly IUnitOfWork _unitOfWork;

        public SkinAnswerSerie(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SkinAnswer>> GetAllSkinAnswerAsync()
        {
            return await _unitOfWork.Repository<SkinAnswer>().ListAllAsync();
        }

        public async Task<SkinAnswer> GetSkinAnswerByIdAsync(int id)
        {
            return await _unitOfWork.Repository<SkinAnswer>().GetByIdAsync(id);
        }

        public async Task AddSkinAnswerAsync(SkinAnswer skinAnswer)
        {
            await _unitOfWork.Repository<SkinAnswer>().AddAsync(skinAnswer);
            skinAnswer.SkinAnswerStatus = SkinAnswerStatus.Active.ToString(); // Gán trạng thái sau khi thêm mới
            await _unitOfWork.Complete();
        }

        public async Task UpdateSkinAnswerAsync(SkinAnswer skinAnswer)
        {
            _unitOfWork.Repository<SkinAnswer>().Update(skinAnswer);
            skinAnswer.SkinAnswerStatus = SkinAnswerStatus.Active.ToString();
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
        }
    }
}
