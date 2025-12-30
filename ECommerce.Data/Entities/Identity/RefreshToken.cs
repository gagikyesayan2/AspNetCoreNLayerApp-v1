using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.Entities.Identity;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; } = null!;
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;

    public bool IsRevoked { get; private set; }
    public DateTime? RevokedAtUtc { get; private set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public void Revoke()
    {
        IsRevoked = true;
        RevokedAtUtc = DateTime.UtcNow;
    }
}

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder
                .HasOne(r => r.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

    }
}