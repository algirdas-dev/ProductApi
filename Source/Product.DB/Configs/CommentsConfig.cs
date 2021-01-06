using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.DB.Models;

namespace Product.DB.Configs
{
    public static class CommentsConfig
    {
        public static void Configs(this EntityTypeBuilder<Comment> model) {
            model.ToTable("Comments");
            model.HasKey(c => c.CommentId);
            model.HasOne(c => c.Product).WithMany(p => p.Comments).HasForeignKey(c=>c.ProductId);
            model.Property(c => c.PosterName).HasMaxLength(50).IsRequired();
            model.Property(c => c.Description).HasMaxLength(500).IsRequired();
            model.Property(c => c.IsDeleted).HasDefaultValue(false);
            model.Property(c => c.IsEnabled).HasDefaultValue(false);
        }
    }
}
