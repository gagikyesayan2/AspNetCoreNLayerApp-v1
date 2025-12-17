namespace Ecommerce.Data.Entities.Identity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public int? Age { get; set; }

        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    }
}
