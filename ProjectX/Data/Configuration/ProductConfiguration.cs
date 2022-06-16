using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectX.Entities.Models;

namespace ProjectX.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        void IEntityTypeConfiguration<Product>.Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired(true).HasColumnName("ProductName");
            builder.Property(p => p.Price).IsRequired(true);
            
        }
    }
}
