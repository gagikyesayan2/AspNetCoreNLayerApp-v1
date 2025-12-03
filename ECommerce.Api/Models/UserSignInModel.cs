using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Models
{
    public class UserSignInModel
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
