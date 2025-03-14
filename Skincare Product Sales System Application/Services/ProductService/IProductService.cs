using Skincare_Product_Sales_System_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Application.Services.ProductService
{
	public interface IProductService
	{
		Task<List<Product>> GetListProducts();
		Product GetProductById(int id);
		Task CreateProduct(Product product);
		void  UpdateProduct( Product product);
		void DeleteProduct(int id);
	}
}
