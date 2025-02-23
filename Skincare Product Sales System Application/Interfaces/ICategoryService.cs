using Skincare_Product_Sales_System_Application.DTOs;
using Skincare_Product_Sales_System_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Interfaces
{
    public interface ICategoryService : IService<Category>
    {
        // Nếu có logic đặc thù cho Category, khai báo ở đây
    }
}
