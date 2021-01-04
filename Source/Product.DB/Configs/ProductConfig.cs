using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.DB.Configs
{
    public static class ProductConfig
    {
        public static void Configs(this EntityTypeBuilder<Models.Product> builder) {
            builder.ToTable("Products");
            builder.HasKey(p=> p.ProductId);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(6).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500);
        }
    }
}
