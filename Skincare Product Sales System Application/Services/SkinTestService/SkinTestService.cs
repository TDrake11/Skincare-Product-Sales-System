using Microsoft.EntityFrameworkCore;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.SkinTestService
{
    public class SkinTestService : ISkinTestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SkinTestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SkinTest>> GetListSkinTests()
        {
            var skinTest = _unitOfWork.Repository<SkinTest>().GetAll()
                .Include(sta => sta.Customer)
                .Include(sta => sta.SkinType);

            return await skinTest.ToListAsync();
        }


        public async Task<List<SkinTest>> GetListSkinTestsByCustomerId(string customerId)
        {
            var skinTest = _unitOfWork.Repository<SkinTest>().GetAll()
                .Include(c => c.SkinType)
                .Include(c => c.Customer)
                .Where(c => c.CustomerId == customerId);

            return await skinTest.ToListAsync();
        }


        public async Task<SkinTest> CreateSkinTestAsync(string customerId, int skinTypeId, List<int> answerIds)
        {
            var existingSkinTests = await _unitOfWork.Repository<SkinTest>()
                .ListAsync(st => st.CustomerId == customerId, null);

            if (existingSkinTests.Any())
            {
                bool hasUpdate = false;
                foreach (var test in existingSkinTests)
                {
                    if (test.SkinTestStatus != "Inactive")
                    {
                        test.SkinTestStatus = "Inactive";
                        _unitOfWork.Repository<SkinTest>().Update(test);
                        hasUpdate = true;
                    }
                }

                if (hasUpdate)
                {
                    await _unitOfWork.Complete();
                }
            }

            var skinAnswers = await _unitOfWork.Repository<SkinAnswer>()
                .ListAsync(filter: a => answerIds.Contains(a.Id), orderBy: null, includeProperties: null);

            var newSkinTest = new SkinTest
            {
                CustomerId = customerId,
                SkinTypeId = skinTypeId,
                CreateDate = DateTime.UtcNow,
                SkinTestStatus = "Active",
                SkinTestAnswer = skinAnswers.Select(a => new SkinTestAnswer
                {
                    SkinAnswer = a,
                    QuestionId = a.QuestionId
                }).ToList()
            };

            await _unitOfWork.Repository<SkinTest>().AddAsync(newSkinTest);
            await _unitOfWork.Complete();

            return newSkinTest;
        }
    }
}
