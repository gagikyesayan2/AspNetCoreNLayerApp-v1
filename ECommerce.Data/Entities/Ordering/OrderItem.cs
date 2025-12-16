using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Data.Entities.Catalog;

namespace Ecommerce.Data.Entities.Ordering
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; } = null!;

        public int Quantity { get; set; }

        // IMPORTANT: snapshot price at purchase time
        public decimal UnitPrice { get; set; }
    }

}
