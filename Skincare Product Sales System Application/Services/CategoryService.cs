using Skincare_Product_Sales_System_Application.DTOs;
using Skincare_Product_Sales_System_Application.Interfaces;
using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Enums;
using Skincare_Product_Sales_System_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        public CategoryService(IRepository<Category> repository) : base(repository)
        {
        }

        // Nếu cần logic đặc biệt cho Category, thêm ở đây --- nên sử lý DTO và Entity ở đây vì nơi này dao thương với Reposirory
    }
}
