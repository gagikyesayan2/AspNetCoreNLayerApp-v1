using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Business.DTOs;

public class UserSignUpDto
{
    public string? Name { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

}

