using Ecommerce.Api.Models.Token;
using Ecommerce.Api.Models.User;
using Ecommerce.Business.DTOs.Token;
using Ecommerce.Business.DTOs.User;
using Ecommerce.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController(IAuthService userRegistrationService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] UserSignUpRequestModel requestModel)
    {

        var dto = new UserSignUpRequestDto
        {
            Email = requestModel.Email,
            Name = requestModel.Name,
            Password = requestModel.Password
        };

        var result = await userRegistrationService.SignUpAsync(dto);

        var responseModel = new UserSignUpResponseModel
        {
            Name = result.Name,
            Email = result.Email
        };
        return Ok(responseModel);
    }

    [HttpPost]
    public async Task<ActionResult> SignIn([FromBody] UserSignInRequestModel requestModel)
    {

        var dto = new UserSignInRequestDto
        {
            Email = requestModel.Email,
            Password = requestModel.Password
        };

        var result = await userRegistrationService.SignInAsync(dto);

        var responseModel = new UserSignInResponseModel
        {
            RefreshToken = result.RefreshToken,
            AccessToken = result.AccessToken,
            Email = result.Email

        };
        return Ok(responseModel);

    }

    [HttpPost]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequestModel requestModel)
    {

        var dto = new RefreshTokenRequestDto
        {
            RefreshToken = requestModel.RefreshToken
        };

        var result = await userRegistrationService.ValidateRefreshTokenAsync(dto);

        var responseModel = new RefreshTokenResponseModel
        {
            RefreshToken = result.RefreshToken,
            AccessToken = result.AccessToken
        };
        return Ok(responseModel);
    }

    [Authorize]
    [HttpGet("profile")]
    public IActionResult GetProfile()
    {

        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        return Ok(new { Email = email, Message = "This is a protected endpoint" });
    }
}
