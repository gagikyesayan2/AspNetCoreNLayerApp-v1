using n_tier_app.Data.Entities;
using n_tier_app.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_tier_app.Data.Interfaces
{

    public interface IUserRepository
    {
        public Task SaveUser(User user);

        public User GetByEmail(string email);
        public User GetByPassword(string email);

        public Task SaveRefreshToken(RefreshToken refreshToken);

        public Task<RefreshToken> GetRefreshToken(int id);
        public Task<RefreshToken> FindRefreshTokenMatch(string refreshToken);

    }
}
