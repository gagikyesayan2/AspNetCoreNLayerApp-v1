using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Models.User;
public class UserSignInRequestModel
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
