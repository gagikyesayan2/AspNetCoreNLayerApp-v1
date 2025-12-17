using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Models.Category
{
    public class CategoryUpdateModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
