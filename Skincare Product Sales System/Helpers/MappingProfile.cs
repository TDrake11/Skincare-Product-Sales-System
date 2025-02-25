using AutoMapper;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Domain.Entities;

namespace Skincare_Product_Sales_System.Helpers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, ProductModel>()
				.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
				.ForMember(dest => dest.SkinTypeName, opt => opt.MapFrom(src => src.SkinType.SkinTypeName))
				.ReverseMap();
			CreateMap<User, RegisterModel>().ReverseMap();
      CreateMap<User, RegisterModel>().ReverseMap();
      CreateMap<Category, CategoryModel>().ReverseMap();
      CreateMap<Comment, CommentModel>().ReverseMap();
			CreateMap<Order, OrderModel>().ReverseMap();
			CreateMap<OrderDetail, OrderDetailModel>().ReverseMap();
		}
   }
}
