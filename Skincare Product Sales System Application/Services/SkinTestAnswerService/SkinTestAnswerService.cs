using Microsoft.EntityFrameworkCore;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.SkinTestAnswerService
{
    public class SkinTestAnswerService : ISkinTestAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SkinTestAnswerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SkinTestAnswer>> GetListSkinTestAnswersAsync()
        {
            var skinTAs = _unitOfWork.Repository<SkinTestAnswer>().GetAll()
                .Include(sta => sta.SkinTest)
                .Include(sta => sta.SkinQuestion)
                .Include(sta => sta.SkinAnswer);

            return await skinTAs.ToListAsync();
        }

        public async Task<List<SkinTestAnswer>> GetListSkinTestAnswersBySkinTestIdAsync(int skinTestId)
        {
            var skinTest = _unitOfWork.Repository<SkinTestAnswer>().GetAll()
                .Include(sta => sta.SkinTest)
                .Include(sta => sta.SkinQuestion)
                .Include(sta => sta.SkinAnswer)
                .Where(c => c.SkinTestId == skinTestId);

            return await skinTest.ToListAsync();
        }
    }
}
