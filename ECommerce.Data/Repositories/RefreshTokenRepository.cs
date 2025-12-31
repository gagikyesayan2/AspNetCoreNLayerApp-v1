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

        public async Task<RefreshToken?> GetLatestValidAsync(int userId)
        {
            var now = DateTime.UtcNow;

            return await _context.RefreshTokens
                .AsNoTracking()
                .Where(t => t.UserId == userId
                            && !t.IsRevoked
                            && t.Expires > now)
                .OrderByDescending(t => t.Id) 
                .FirstOrDefaultAsync();
        }
        public async Task<RefreshToken?> FindMatchAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token == token);

            return refreshToken;
        }




    }
}
