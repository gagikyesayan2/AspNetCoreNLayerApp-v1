using AutoMapper;
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
    private readonly IMapper _mapper;

    public AuthService(IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IOptions<JwtSettings> jwtOptions, IMapper mapper)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtSettings = jwtOptions.Value;
        _mapper = mapper;
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
    public async Task<RefreshTokenResponseDto> ValidateRefreshTokenAsync(RefreshTokenRequestDto requestDto)
    {
        var oldRefreshToken = await _refreshTokenRepository.FindMatchAsync(requestDto.RefreshToken);

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


        var responseDto = _mapper.Map<RefreshTokenResponseDto>(newRefreshToken);
        responseDto.AccessToken = newAccessToken;
        return responseDto;
}


    public async Task<UserSignInResponseDto> SignInAsync(UserSignInRequestDto requestDto)
    {
        var user = await _userRepository.GetByEmailAsync(requestDto.Email!);

        if (user == null || !BCrypt.Net.BCrypt.Verify(requestDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAppException(
                "Wrong email or password.",
                "auth_invalid_credentials"
            );
        }
     
        var accessToken = GenerateAccessToken(user.Id);
        string refreshToken;


        var existingRefreshToken = await _refreshTokenRepository.GetLatestValidAsync(user.Id);
        

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


        var responseDto = _mapper.Map<UserSignInResponseDto>(user);

        responseDto.RefreshToken = refreshToken;
        responseDto.AccessToken = accessToken;
        return responseDto;

    }
    public async Task<UserSignUpResponseDto> SignUpAsync(UserSignUpRequestDto requestDto)
    {

        var result = await _userRepository.GetByEmailAsync(requestDto.Email);

        if (result != null)
        {
            throw new ConflictAppException("Email is already in use.",
                "auth_email_taken");

        }

        var user = _mapper.Map<User>(requestDto);

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(requestDto.Password);


        await _userRepository.SaveAsync(user);

        return _mapper.Map<UserSignUpResponseDto>(user);
    }

}
