using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using n_tier_app.Data.Entities;
using n_tier_app.Data.models;

namespace n_tier_app.Data.Context
{
    public class AppDbContext : DbContext
    {
        // what is internal access modifier, it is used for
        // its scope (inside project/assembly)

        // why public and not internal, because entity freamwork need publicity to inspect
        // code (class) and find necessary data it wants, that's called reflection 
        // reflection: The ability of a program to inspect or modify its own structure
        // at runtime.


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        /*
        DbContextOptions<AppDbContext> options
        This object contains configuration info for EF —
        like the database provider(SQL Server, SQLite, etc.) and connection string.
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(r => r.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(r => r.UserId);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
