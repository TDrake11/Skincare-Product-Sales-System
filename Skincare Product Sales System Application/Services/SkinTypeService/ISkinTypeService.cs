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
        Task<IEnumerable<SkinType>> GetListSkinTypesAsync();
        Task AddSkinTypeAsync(SkinType skinType);
        Task UpdateSkinTypeAsync(SkinType skinType);
        Task DeleteSkinTypeAsync(int id);
    }
}
