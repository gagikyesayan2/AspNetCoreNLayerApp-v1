using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Models.User;
public class UserSignUpRequestModel
{
    [Required]
    public string? Name { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }



}
