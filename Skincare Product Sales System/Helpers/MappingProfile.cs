﻿using AutoMapper;
using Skincare_Product_Sales_System.Models;
using Skincare_Product_Sales_System_Domain.Entities;

namespace Skincare_Product_Sales_System.Helpers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, ProductModel>()
				.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : null))
				.ForMember(dest => dest.SkinTypeName, opt => opt.MapFrom(src => src.SkinType != null ? src.SkinType.SkinTypeName : null))
				.ReverseMap();
			CreateMap<User, RegisterModel>().ReverseMap();
			CreateMap<User, UserTokenModel>().ReverseMap();
			CreateMap<User, UserProfileModel>().ReverseMap();
			CreateMap<Category, CategoryModel>().ReverseMap();
			CreateMap<Comment, CommentModel>().ReverseMap();
			CreateMap<Order, OrderModel>().ReverseMap();
			CreateMap<OrderDetail, OrderDetailModel>().ReverseMap();
			CreateMap<SkinQuestion, SkinQuestionModel>().ReverseMap();
			CreateMap<SkinAnswer, SkinAnswerModel>().ReverseMap();
			CreateMap<SkinType, SkinTypeModel>().ReverseMap();
        }
	}
}
