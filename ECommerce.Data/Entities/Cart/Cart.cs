using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data.Entities.Cart
{
    public class Cart
    {
        public int Id { get; set; }

        public int? UserId { get; set; }


        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }

}
