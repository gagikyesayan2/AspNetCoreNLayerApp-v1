using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Business.DTOs.User;

public class UserSignUpRequestDto
{
    public string Name { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

}

