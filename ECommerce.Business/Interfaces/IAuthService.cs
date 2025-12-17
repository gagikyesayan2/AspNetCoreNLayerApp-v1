using Ecommerce.Business.DTOs.Token;
using Ecommerce.Business.DTOs.User;


namespace Ecommerce.Business.Interfaces
{
    public interface IAuthService
    {
        public Task<UserSignUpResponseDto> SignUpAsync(UserSignUpRequestDto userSignUpDto);
        public Task<UserSignInResponseDto> SignInAsync(UserSignInRequestDto userSignInDto);

        public Task<RefreshTokenResponseDto> ValidateRefreshTokenAsync(RefreshTokenRequestDto refreshToken);
    }
}
