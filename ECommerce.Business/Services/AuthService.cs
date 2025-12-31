using Ecommerce.Business.Config;
using Ecommerce.Business.DTOs.Token;
using Ecommerce.Business.DTOs.User;
using Ecommerce.Business.Exceptions.Common;
using Ecommerce.Business.Interfaces;
using Ecommerce.Data.Entities.Identity;
using Ecommerce.Data.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Business.Services;

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
    public async Task<RefreshTokenResponseDto> ValidateRefreshTokenAsync(RefreshTokenRequestDto responseDto)
    {
        var oldRefreshToken = await _refreshTokenRepository.FindMatchAsync(responseDto.RefreshToken);

        if (oldRefreshToken is null || oldRefreshToken.IsExpired || oldRefreshToken.IsRevoked)
        {
            throw new UnauthorizedAppException(
            "Refresh token is invalid. Please sign in again.",
            "auth_refresh_invalid");
        }

        oldRefreshToken.Revoke();

        var newRefreshToken = new RefreshToken
        {
            Token = GenerateRefreshToken(),
            UserId = oldRefreshToken.UserId,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        await _refreshTokenRepository.SaveAsync(newRefreshToken);

        string newAccessToken = GenerateAccessToken(newRefreshToken.UserId);

        return new RefreshTokenResponseDto
        {
            RefreshToken = newRefreshToken.Token,
            AccessToken = newAccessToken
        };


}


    public async Task<UserSignInResponseDto> SignInAsync(UserSignInRequestDto userSignInDto)
    {
        var user = await _userRepository.GetByEmailAsync(userSignInDto.Email!);

        if (user == null || !BCrypt.Net.BCrypt.Verify(userSignInDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAppException(
                "Wrong email or password.",
                "auth_invalid_credentials"
            );
        }
        var accessToken = GenerateAccessToken(user.Id);


        var existingRefreshToken = await _refreshTokenRepository.GetLatestValidAsync(user.Id);
        string refreshToken;

        if (existingRefreshToken != null && !existingRefreshToken.IsExpired && !existingRefreshToken.IsRevoked)
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

        return new UserSignInResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Email = user.Email

        };
    }
    public async Task<UserSignUpResponseDto> SignUpAsync(UserSignUpRequestDto requestDto)
    {

        var result = await _userRepository.GetByEmailAsync(requestDto.Email);

        if (result != null)
        {
            throw new ConflictAppException("Email is already in use.",
                "auth_email_taken");

        }
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(requestDto.Password);

        var user = new User
        {
            Name = requestDto.Name,
            Email = requestDto.Email,
            PasswordHash = passwordHash
        };
        await _userRepository.SaveAsync(user);

        return new UserSignUpResponseDto
        {
            Email = requestDto.Email,
            Name = requestDto.Name,

        };
    }

}
