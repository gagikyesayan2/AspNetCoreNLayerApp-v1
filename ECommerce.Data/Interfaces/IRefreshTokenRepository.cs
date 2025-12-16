using Ecommerce.Data.Entities.Identity;

namespace Ecommerce.Data.Interfaces
{
    public interface IRefreshTokenRepository
    {
        public Task SaveAsync(RefreshToken refreshToken);
        public Task<RefreshToken?> GetAsync(int id);

        public Task<RefreshToken?> FindMatchAsync(string refreshToken);
    }
}
