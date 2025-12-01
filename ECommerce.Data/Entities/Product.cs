using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_tier_app.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }

        public int CategoryId { get; set; }

        // why Category is nullable because
        // sometimes a product may not be categorized yet (admin forgot)
        public Category? Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
