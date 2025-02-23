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

            CreateMap<Product, ProductModel>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
            .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.Staff.UserName))
            .ForMember(dest => dest.SkinTypeName, opt => opt.MapFrom(src => src.SkinType.SkinTypeName))
            .ReverseMap()
            .ForMember(dest => dest.Category, opt => opt.Ignore()) // Tránh lỗi null khi map ngược
            .ForMember(dest => dest.Staff, opt => opt.Ignore())
            .ForMember(dest => dest.SkinType, opt => opt.Ignore());
        }
	}
}
