using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.DB.Configs
{
    public static class ProductConfig
    {
        public static void Configs(this EntityTypeBuilder<Models.Product> model) {
            model.ToTable("Products");
            model.HasKey(p=> p.ProductId);
            model.Property(p => p.Name).HasMaxLength(50).IsRequired();
            model.Property(p => p.Code).HasMaxLength(6).IsRequired();
            model.Property(p => p.Description).HasMaxLength(500);
            model.Property(p => p.IsDeleted).HasDefaultValue(false);
            model.Property(p => p.IsEnabled).HasDefaultValue(false);
        }
    }
}
