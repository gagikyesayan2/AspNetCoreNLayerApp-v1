using Microsoft.EntityFrameworkCore;
using Ecommerce.Data.Interfaces;
using Ecommerce.Data.Context;
using Ecommerce.Data.Entities.Identity;

namespace Ecommerce.Data.Repositories
{

    // what is internal access modifier, it is used for
    // its scope (inside project/assembly)

    // why public and not internal, because entity freamwork need publicity to inspect
    // code (class) and find necessary data it wants, that's called reflection 
    // reflection: The ability of a program to inspect or modify its own structure
    // at runtime.


    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task SaveRefreshToken(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetRefreshToken(int id)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(token => token.UserId == id);
        }
        public async Task<RefreshToken?> FindRefreshTokenMatch(string refreshToken)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(token => token.Token == refreshToken);
        }
        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(user => user.Email == email)!;
        }
        public User GetByPassword(string password)
        {
            return _context.Users.FirstOrDefault(user => user.PasswordHash == password)!;
        }



    }
}
