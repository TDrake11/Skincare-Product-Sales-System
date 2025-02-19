using Skincare_Product_Sales_System_Domain.Entities;
using Skincare_Product_Sales_System_Domain.Repositories;
using Skincare_Product_Sales_System_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Infrastructure.Repositories
{
    public class ProductRepository(ApplicationDbContext _context) : Repository<Product>(_context), IProductRepository
    {
        public override async Task<IEnumerable<Product>> GetAll()
        {
            return await base.GetAll();
        }

        
    }
}
