using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Models.User
{
    public class UserSignUpResponseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
