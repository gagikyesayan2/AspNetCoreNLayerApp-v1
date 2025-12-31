using AutoMapper;
using Ecommerce.Api.Models.Category;
using Ecommerce.Api.Models.Token;
using Ecommerce.Api.Models.User;
using Ecommerce.Business.DTOs.Category;
using Ecommerce.Business.DTOs.Token;
using Ecommerce.Business.DTOs.User;

namespace Ecommerce.Api.Mapping;
public class ApiMappingProfile : Profile
{

    public ApiMappingProfile()
    {
        //user
        CreateMap<UserSignUpRequestModel, UserSignUpRequestDto>();

        CreateMap<UserSignUpResponseDto, UserSignUpResponseModel>();

        CreateMap<UserSignInRequestModel, UserSignInRequestDto>();

        CreateMap<UserSignInResponseDto, UserSignInResponseModel>();

        CreateMap<RefreshTokenRequestModel, RefreshTokenRequestDto>();

        CreateMap<RefreshTokenResponseDto, RefreshTokenResponseModel>();
        //category

        CreateMap<CategoryCreateModel, CategoryCreateDto>();

        CreateMap<CategoryReadDto, CategoryReadModel>();

        CreateMap<CategoryUpdateModel, CategoryUpdateDto>();

        CreateMap<CategoryUpdateDto, CategoryUpdateModel>();

        CreateMap<CategoryReadDto, CategoryUpdateModel>();
    }
}
