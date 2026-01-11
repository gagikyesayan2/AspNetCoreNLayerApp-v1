using Ecommerce.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data.Entities.Catalog;

namespace Ecommerce.Data.Entities.Catalog
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();


    }
}
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .HasIndex(c => c.Name)
            .IsUnique();

    }
}