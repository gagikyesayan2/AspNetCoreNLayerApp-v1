using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Models.Token
{
    public class RefreshTokenRequestModel
    {
        [Required]
        public string RefreshToken { get; set; }

    }
}
