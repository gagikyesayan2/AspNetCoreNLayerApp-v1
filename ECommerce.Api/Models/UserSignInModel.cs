using System.ComponentModel.DataAnnotations;

namespace n_tier_app.Api.Models
{
    public class UserSignInModel
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
