using Ecommerce.Data.Context;
using Ecommerce.Data.Entities.Identity;
using Ecommerce.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }



    }
}
