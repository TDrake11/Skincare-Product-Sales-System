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
        Task<IEnumerable<SkinAnswer>> GetAllSkinAnswerAsync();
        Task<SkinAnswer> GetSkinAnswerByIdAsync(int id);
        Task AddSkinAnswerAsync(SkinAnswer skinAnswer);
        Task UpdateSkinAnswerAsync(SkinAnswer skinAnswer);
        Task DeleteSkinAnswerAsync(int id);
    }
}
