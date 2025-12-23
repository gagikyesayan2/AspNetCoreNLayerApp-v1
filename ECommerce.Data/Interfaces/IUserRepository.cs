using Ecommerce.Data.Entities.Identity;

namespace Ecommerce.Data.Interfaces
{

    public interface IUserRepository
    {
        public Task SaveAsync(User user);

        public Task<User?> GetByEmailAsync(string email);


    }
}
