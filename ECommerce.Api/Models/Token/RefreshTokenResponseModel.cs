using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Models.Token
{
    public class RefreshTokenResponseModel
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
