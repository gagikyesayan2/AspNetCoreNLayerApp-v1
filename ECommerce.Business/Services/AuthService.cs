using Ecommerce.Business.Config;
using Ecommerce.Business.DTOs;
using Ecommerce.Business.Interfaces;
using Ecommerce.Data.Entities.Identity;
using Ecommerce.Data.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Business.Services
{

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IOptions<JwtSettings> jwtOptions)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtSettings = jwtOptions.Value;
        }



        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        private string GenerateAccessToken(int userId)
        {
            var claims = new[]
            {
              new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
              new Claim(ClaimTypes.Role, "User")
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer: _jwtSettings.Issuer,
              audience: _jwtSettings.Audience,
              claims: claims,
              expires: DateTime.Now.AddHours(1),
              signingCredentials: credentials
          );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<object> ValidateRefreshToken(string refreshToken)
        {
            var refreshTokenEntity = await _refreshTokenRepository.FindMatchAsync(refreshToken);

            if (refreshTokenEntity == null)
            {
                throw new InvalidOperationException("login to get a new refresh token.");
            }
            else if (refreshTokenEntity.IsExpired)
            {
                string newRefreshToken = GenerateRefreshToken();
                var newRefreshTokenEntity = new RefreshToken
                {
                    Token = newRefreshToken,
                    UserId = refreshTokenEntity.UserId,
                    Expires = DateTime.UtcNow.AddDays(7)
                };

                await _refreshTokenRepository.SaveAsync(newRefreshTokenEntity);
                string newAccessToken = GenerateAccessToken(refreshTokenEntity.UserId);
                return new
                {
                    newRefreshToken,
                    newAccessToken
                };
            }
            else
            {
                throw new InvalidOperationException("you already have a valid refresh token.");
            }


        }

        public async Task<object> SignIn(UserSignInDto userSignInDto)
        {
            var user = _userRepository.GetByEmailAsync(userSignInDto.Email!);

            if (user == null || !BCrypt.Net.BCrypt.Verify(userSignInDto.Password, user.PasswordHash))
            {
                throw new InvalidOperationException("wrong email or password.");
            }
            var accessToken = GenerateAccessToken(user.Id);

            //  Generate JWT token
            var existingRefreshToken = await _refreshTokenRepository.GetAsync(user.Id);
            string refreshToken;

            if (existingRefreshToken != null && !existingRefreshToken.IsExpired)
            {
                refreshToken = existingRefreshToken.Token;
            }
            else
            {
                refreshToken = GenerateRefreshToken();
                var RefreshTokenEntity = new RefreshToken
                {
                    Token = refreshToken,
                    UserId = user.Id,
                    Expires = DateTime.UtcNow.AddDays(7)
                };
                await _refreshTokenRepository.SaveAsync(RefreshTokenEntity);

            }

            return new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken

            };
        }
        public async Task<UserSignUpDto> SignUp(UserSignUpDto userSignUpDto)
        {

            var result = _userRepository.GetByEmailAsync(userSignUpDto.Email!);

            if (result != null)
            {
                throw new InvalidOperationException("Email is already in use.");

            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userSignUpDto.Password);

            var user = new User
            {
                Name = userSignUpDto.Name,
                Email = userSignUpDto.Email,
                PasswordHash = passwordHash
            };
            await _userRepository.SaveAsync(user);

            return new UserSignUpDto
            {
                Name = userSignUpDto.Name

            };
        }

    }
}
