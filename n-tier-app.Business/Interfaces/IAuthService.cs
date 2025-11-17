using n_tier_app.Business.Dto;
using n_tier_app.Business.DTOs;


namespace n_tier_app.Business.Interfaces
{
    public interface IAuthService
    {
        public Task<UserSignUpDto> SignUp(UserSignUpDto userSignUpDto);
        public Task<object> SignIn(UserSignInDto userSignInDto);

        public Task<object> ValidateRefreshToken(string refreshToken);
    }
}
