using Skincare_Product_Sales_System_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.SkinTypeService
{
    public interface ISkinTypeService
    {
        Task<List<SkinType>> GetListSkinTypes();
        Task<SkinType> GetSkinTypeById(int id);
        Task AddSkinType(SkinType skinType);
        Task UpdateSkinType(SkinType skinType);
        Task DeleteSkinType(int id);

        

    }
}
