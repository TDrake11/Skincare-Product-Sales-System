using Skincare_Product_Sales_System_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.SkinQuestionService
{
    public interface ISkinQuestionService
    {
        Task<IEnumerable<SkinQuestion>> GetAllSkinQuestionAsync();
        Task<SkinQuestion?> GetSkinQuestionByIdAsync(int id);
        Task AddSkinQuestionAsync(SkinQuestion skinQuestion);
        Task UpdateSkinQuestionAsync(SkinQuestion skinQuestion);
        Task DeleteSkinQuestionAsync(int id);
    }
}
