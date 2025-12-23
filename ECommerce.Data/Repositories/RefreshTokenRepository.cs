using Ecommerce.Data.Context;
using Ecommerce.Data.Entities.Identity;
using Ecommerce.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetAsync(int userId)
        {
            var refreshToken = await _context.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.UserId == userId);
            return refreshToken;
        }
        public async Task<RefreshToken?> FindMatchAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Token == token);

            return refreshToken;
        }




    }
}
