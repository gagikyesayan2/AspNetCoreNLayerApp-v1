using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data.Entities.Ordering
{
    public enum OrderStatus
    {
        Pending = 1,
        Paid = 2,
        Shipped = 3,
        Cancelled = 4
    }
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        // Navigation
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

}
