using Skincare_Product_Sales_System_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.SkinAnswerService
{
    public interface ISkinAnswerSerie
    {
        Task<List<SkinAnswer>> GetAllSkinAnswer();
        Task<SkinAnswer> GetSkinAnswerById(int id);
        Task<List<SkinAnswer>> GetSkinAnswersBySkinTypeId(int skinTypeId);
        Task<List<SkinAnswer>> GetSkinAnswersBySkinQuestionId(int skinQuestionId);
        Task AddSkinAnswer(SkinAnswer skinAnswer);
        Task UpdateSkinAnswer(SkinAnswer skinAnswer);
        Task DeleteSkinAnswer(int id);
    }
}
