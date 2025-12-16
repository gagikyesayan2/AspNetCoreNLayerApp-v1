using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Models.Category
{
    public class CategoryCreateModel
    {

        [Required]
        [MaxLength(120)]
        public string? Name { get; set; } = null!;


        [Required]
        [MaxLength(400)]
        public string? Description { get; set; } = null!;
    }
}



