using AutoMapper;
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
public class AuthController(IAuthService userRegistrationService, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] UserSignUpRequestModel requestModel)
    {

        var requestDto = mapper.Map<UserSignUpRequestDto>(requestModel);

        var responseDto = await userRegistrationService.SignUpAsync(requestDto);

        var responseModel = mapper.Map<UserSignUpResponseModel>(responseDto);

        return Ok(responseModel);
    }

    [HttpPost]
    public async Task<ActionResult> SignIn([FromBody] UserSignInRequestModel requestModel)
    {

    
        var requestDto = mapper.Map<UserSignInRequestDto>(requestModel);

        var responseDto = await userRegistrationService.SignInAsync(requestDto);

        var responseModel = mapper.Map<UserSignInResponseModel>(responseDto);

        return Ok(responseModel);

    }

    [HttpPost]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequestModel requestModel)
    {
        var requestDto = mapper.Map<RefreshTokenRequestDto>(requestModel);
      
        var responseDto = await userRegistrationService.ValidateRefreshTokenAsync(requestDto);

     
        var responseModel = mapper.Map<RefreshTokenResponseModel>(responseDto);

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
