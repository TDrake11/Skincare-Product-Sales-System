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
		Product GetProductDeatilById(int id);
		Task CreateProduct(Product product);
		Task  UpdateProduct( Product product);
		Task DeleteProduct(Product product);
	}
}
