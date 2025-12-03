
using Ecommerce.Business.DTOs;


namespace Ecommerce.Business.Interfaces
{
    public interface IAuthService
    {
        public Task<UserSignUpDto> SignUp(UserSignUpDto userSignUpDto);
        public Task<object> SignIn(UserSignInDto userSignInDto);

        public Task<object> ValidateRefreshToken(string refreshToken);
    }
}
