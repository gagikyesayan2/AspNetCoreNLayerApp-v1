using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.Entities.Catalog;
public class ProductVariant
{
    public int Id { get; set; }
    public string Sku { get; set; }
    public decimal Price { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; } = null!;
}


public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.HasOne(v => v.Product)
            .WithMany(p => p.productVariants)
            .HasForeignKey(v => v.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(v => v.Price)
            .HasPrecision(18, 2);
    }
}
