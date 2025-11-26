using System.ComponentModel.DataAnnotations;

namespace n_tier_app.Business.DTOs;

public class UserSignUpDto
{
    public string? Name { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

}

