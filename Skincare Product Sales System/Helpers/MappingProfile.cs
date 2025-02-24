using AutoMapper;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Domain.Entities;

namespace Skincare_Product_Sales_System.Helpers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
            CreateMap<User, RegisterModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
			CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<CommentModel, Comment>().ReverseMap();
        }
	}
}
