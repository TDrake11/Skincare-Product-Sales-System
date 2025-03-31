using Skincare_Product_Sales_System_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.SkinTestAnswerService
{
    public interface ISkinTestAnswerService
    {
        Task<IEnumerable<SkinTestAnswer>> GetListSkinTestAnswersAsync();
        Task<List<SkinTestAnswer>> GetListSkinTestAnswersBySkinTestIdAsync(int skinTestId);
    }
}
