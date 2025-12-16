using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data.Entities.Catalog
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // nullable "?", description might be null
        public string? Description { get; set; }

        // collection of many products (one to many)
        public ICollection<Product> Products { get; set; } = new List<Product>();


    }
}
