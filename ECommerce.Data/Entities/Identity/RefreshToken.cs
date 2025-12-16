using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data.Entities.Identity
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }

        public DateTime Expires { get; set; }

        //That => is called an expression-bodied property in C#.
        //shorter syntax for a property that only returns a value (no set).
        public bool IsExpired => DateTime.UtcNow >= Expires;

        // navigation property
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
