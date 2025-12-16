using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data.Entities.Catalog
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public string Sku { get; set; } = null!;
        public decimal Price { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;
    }

}
