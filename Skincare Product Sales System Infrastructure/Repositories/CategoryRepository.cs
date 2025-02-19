using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Interfaces;
using Skincare_Product_Sales_System_Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Infrastructure.Repositories
{
    public class CategoryRepository(ApplicationDbContext _context) : Repository<Category>(_context), ICategoryRepository
    {
        public override async Task<IEnumerable<Category>> GetAll()
        {
            return await base.GetAll();
        }
    }
}
