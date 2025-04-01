using Skincare_Product_Sales_System_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.SkinTestService
{
    public interface ISkinTestService
    {
        Task<List<SkinTest>> GetListSkinTestsAsync();
        Task<List<SkinTest>> GetListSkinTestsByCustomerIdAsync(string customerId);
        Task<SkinTest> CreateSkinTestAsync(string customerId, int skinTypeId, List<int> answerIds);
    }
}
