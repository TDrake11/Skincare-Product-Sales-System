using Skincare_Product_Sales_System_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.SkinAnswerService
{
    public interface ISkinAnswerService
    {
        Task<List<SkinAnswer>> GetAllSkinAnswerAsync();
        Task<SkinAnswer?> GetSkinAnswerByIdAsync(int id);
        Task<List<SkinAnswer>> GetSkinAnswersBySkinTypeIdAsync(int skinTypeId);
        Task<List<SkinAnswer>> GetSkinAnswersBySkinQuestionIdAsync(int skinQuestionId);
        Task AddSkinAnswerAsync(SkinAnswer skinAnswer);
        Task UpdateSkinAnswerAsync(SkinAnswer skinAnswer);
        Task DeleteSkinAnswerAsync(int id);
    }
}
