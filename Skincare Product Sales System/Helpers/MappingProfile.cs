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
				.ForMember(dest => dest.SkinTypeName, opt => opt.MapFrom(src => src.SkinType.SkinTypeName));
			CreateMap<ProductModel, Product>();
			CreateMap<Product, ProductUpdateModel>().ReverseMap();
			CreateMap<User, RegisterModel>().ReverseMap();
			CreateMap<User, UserTokenModel>().ReverseMap();
			CreateMap<User, UserProfileModel>().ReverseMap();
			CreateMap<UserProfileUpdateModel, User>().ReverseMap();
			
			CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<CreateCategoryRequest, Category>().ReverseMap();
			CreateMap<UpdateCategoryRequest, Category>().ReverseMap();

            CreateMap<Comment, CommentModel>().ReverseMap();
			CreateMap<Order, OrderModel>().ReverseMap();
			CreateMap<OrderDetail, OrderDetailModel>().ReverseMap();
			CreateMap<SkinQuestion, SkinQuestionModel>().ReverseMap();
			CreateMap<SkinAnswer, SkinAnswerModel>()
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.SkinQuestion.QuestionText))
                .ForMember(dest => dest.SkinTypeName, opt => opt.MapFrom(src => src.SkinType.SkinTypeName))
                .ReverseMap();
            CreateMap<SkinType, SkinTypeModel>().ReverseMap();

			CreateMap<StepRoutine, StepRoutineModel>()
                .ForMember(dest => dest.RoutineName, opt => opt.MapFrom(src => src.Routine.RoutineName))
				.ReverseMap();
			CreateMap<StepRoutine, CreateStepRoutineModel>() .ReverseMap();
			CreateMap<StepRoutine, UpdateStepRoutineModel>() .ReverseMap();
			CreateMap<Order, CartModel>();
			CreateMap<OrderDetail, CartDetailModel>()
				.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
				.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Product.Image));
		}
	}
}