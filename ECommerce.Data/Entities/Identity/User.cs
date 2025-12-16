using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data.Entities.Identity
{
    // what is internal access modifier, it is used for
    // its scope (inside project/assembly)

    // why public and not internal, because entity freamwork need publicity to inspect
    // code (class) and find necessary data it wants, that's called reflection 
    // reflection: The ability of a program to inspect or modify its own structure
    // at runtime.


    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public int? Age { get; set; }

        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        // Add this to link (Navigation property on) RefreshTokens 
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    }
}
