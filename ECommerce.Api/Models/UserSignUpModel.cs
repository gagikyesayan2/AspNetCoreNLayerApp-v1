using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Models
{
    public class UserSignUpModel
    {
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
  
        public string? Password { get; set; }


     

    }
}
