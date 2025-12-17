

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Models.Category
{
    public class CategoryReadModel
    {

        [Required]
        public int Id { get; set; }
        [Required]
        public string ?Name { get; set; }
        [Required]
        public string ?Description { get; set; }

    }
}
