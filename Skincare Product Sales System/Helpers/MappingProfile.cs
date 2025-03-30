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
				.ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.Staff.FirstName + src.Staff.LastName));
			CreateMap<ProductModel, Product>();
			CreateMap<Product, ProductUpdateModel>().ReverseMap();
			CreateMap<User, RegisterModel>().ReverseMap();
			CreateMap<User, UserTokenModel>().ReverseMap();
			CreateMap<User, UserProfileModel>().ReverseMap();
			CreateMap<UserProfileUpdateModel, User>().ReverseMap();
			
			CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<CreateCategoryRequest, Category>().ReverseMap();

            CreateMap<Comment, CommentModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Customer.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Customer.LastName))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Customer.Avatar)).ReverseMap();
			CreateMap<Comment, CreateCommentModel>().ReverseMap();
			CreateMap<Comment, UpdateCommentModel>().ReverseMap();

			CreateMap<Order, OrderModel>().ReverseMap();
			CreateMap<Order, UpdateOrderModel>().ReverseMap();

			CreateMap<OrderDetail, OrderDetailModel>().ReverseMap();

			CreateMap<SkinQuestion, SkinQuestionModel>().ReverseMap();
			CreateMap<SkinQuestion, CreateSkinQuestionModel>().ReverseMap();
			CreateMap<SkinQuestion, UpdateSkinQuestionModel>().ReverseMap();

			CreateMap<SkinAnswer, SkinAnswerModel>()
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.SkinQuestion.QuestionText))
                .ForMember(dest => dest.SkinTypeName, opt => opt.MapFrom(src => src.SkinType.SkinTypeName))
                .ReverseMap();
			CreateMap<SkinAnswer, CreateSkinAnswerModel>().ReverseMap();
			CreateMap<SkinAnswer, UpdateSkinAnswerModel>().ReverseMap();

            CreateMap<SkinType, SkinTypeModel>().ReverseMap();
            CreateMap<SkinType, CreateSkinTypeModel>().ReverseMap();  

			CreateMap<SkinTest, SkinTestModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Customer.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Customer.LastName))
                .ForMember(dest => dest.SkinTypeName, opt => opt.MapFrom(src => src.SkinType.SkinTypeName)).ReverseMap();
            CreateMap<SkinTest, CreateSkinTestModel>().ReverseMap();

            CreateMap<SkinTestAnswer, SkinTestAnswerModel>()
				.ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.SkinQuestion.QuestionText))
				.ForMember(dest => dest.AnswerText, opt => opt.MapFrom(src => src.SkinAnswer.AnswerText))
				.ReverseMap();

            CreateMap<SkinCareRoutine, SkinCareRoutineModel>()
                .ForMember(dest => dest.SkinTypeName, opt => opt.MapFrom(src => src.SkinType.SkinTypeName))
				.ReverseMap();
			CreateMap<SkinCareRoutine, CreateSkinCareRoutineModel>().ReverseMap();
			CreateMap<SkinCareRoutine, UpdateSkinCareRoutineModel>().ReverseMap();

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