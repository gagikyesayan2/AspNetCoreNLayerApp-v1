using AutoMapper;
using Ecommerce.Business.DTOs.Category;
using Ecommerce.Business.DTOs.Token;
using Ecommerce.Business.DTOs.User;
using Ecommerce.Data.Entities.Catalog;
using Ecommerce.Data.Entities.Identity;


namespace Ecommerce.Business.Mapping;
public sealed class BusinessMappingProfile : Profile
{
    public BusinessMappingProfile()
    {
        //token 

        CreateMap<RefreshToken, RefreshTokenResponseDto>()
        .ForMember(d => d.RefreshToken, o => o.MapFrom(s => s.Token))
        .ForMember(d => d.AccessToken, o => o.Ignore());

        // user
        CreateMap<UserSignUpRequestDto, User>()
            .ForMember(d => d.PasswordHash, opt => opt.Ignore());

        CreateMap<User, UserSignUpResponseDto>();
        CreateMap<User, UserSignInResponseDto>()
        .ForMember(d => d.AccessToken, opt => opt.Ignore())
        .ForMember(d => d.RefreshToken, opt => opt.Ignore());

        // category 
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>()
          .ForMember(d => d.Id, opt => opt.Ignore());

        // Map FROM Category TO CategoryReadDto 
        CreateMap<Category, CategoryReadDto>();
    }
}
