using Microsoft.AspNetCore.Mvc;
using n_tier_app.Api.Models;
using n_tier_app.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using n_tier_app.Business.DTOs;

namespace n_tier_app.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController(IAuthService userRegistrationService) : ControllerBase
    {


        [HttpPost]
        public async Task<ActionResult> SignUp([FromBody] UserSignUpModel userSignUpModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            UserSignUpDto userDto = new UserSignUpDto
            {
                Email = userSignUpModel.Email,
                Name = userSignUpModel.Name,
                Password = userSignUpModel.Password
            };

            var result = await userRegistrationService.SignUp(userDto);

            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> SignIn([FromBody] UserSignInModel userSignInModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            UserSignInDto userDto = new UserSignInDto
            {
                Email = userSignInModel.Email,
                Password = userSignInModel.Password
            };

            var result = await userRegistrationService.SignIn(userDto);
            Console.WriteLine(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var result = await userRegistrationService.ValidateRefreshToken(refreshToken);
            
            return Ok(result);
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            // You can access claims from the token
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            return Ok(new { Email = email, Message = "This is a protected endpoint" });
        }
    }

}
