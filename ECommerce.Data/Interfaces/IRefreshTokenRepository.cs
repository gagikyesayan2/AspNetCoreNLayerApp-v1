using Ecommerce.Data.Entities.Identity;

namespace Ecommerce.Data.Interfaces
{
    public interface IRefreshTokenRepository
    {
        public Task SaveAsync(RefreshToken refreshToken);
        public Task<RefreshToken?> GetLatestValidAsync(int userId);

        public Task<RefreshToken?> FindMatchAsync(string token);
    }
}
