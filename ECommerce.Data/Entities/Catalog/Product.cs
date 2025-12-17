using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data.Entities.Identity;

namespace Ecommerce.Data.Entities.Catalog;

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } 
        public string? Description { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<ProductVariant> productVariants = new List<ProductVariant>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }


public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
public void Configure(EntityTypeBuilder<Product> builder)
{
    builder.HasOne(p => p.Category)
        .WithMany(c => c.Products)
        .HasForeignKey(p => p.CategoryId)
        .OnDelete(DeleteBehavior.Restrict);

    builder.Property(p => p.Price)
        .HasPrecision(18, 2);
}
}