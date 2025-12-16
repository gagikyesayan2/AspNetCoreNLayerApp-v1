using Ecommerce.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data.Interfaces
{

    public interface IUserRepository
    {
        public Task SaveAsync(User user);

        public User GetByEmailAsync(string email);
       

    }
}
