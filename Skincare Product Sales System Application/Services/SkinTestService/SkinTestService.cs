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
            return await _unitOfWork.Repository<SkinTest>().ListAsync(
                includeProperties: query => query
                    .Include(sta => sta.Customer)
                    .Include(sta => sta.SkinType)
            );
        }
        public async Task<SkinTest> GetSkinTestById(int id)
        {
            var skinTest = await _unitOfWork.Repository<SkinTest>()
                .GetAll()
                .Include(st => st.Customer)
                .Include(st => st.SkinType)
                .AsNoTracking()
                .FirstOrDefaultAsync(st => st.Id == id);
            return skinTest;
        }

        public async Task AddSkinTest(SkinTest skinTest)
        {
            var existingSkinTest = await _unitOfWork.Repository<SkinTest>()
                .ListAsync(st => st.CustomerId == skinTest.CustomerId, null);

            if (existingSkinTest.Any())
            {
                bool hasUpdate = false;
                foreach (var test in existingSkinTest)
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
            skinTest.SkinTestStatus = "Active";
            skinTest.CreateDate = DateTime.Now;
            await _unitOfWork.Repository<SkinTest>().AddAsync(skinTest);
            await _unitOfWork.Complete();
        }
    }
}
