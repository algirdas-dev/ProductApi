using Microsoft.EntityFrameworkCore;
using Product.DB.Models;
using Product.DB.Configs;

namespace Product.DB
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options)
        : base(options)
        { }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlServer("Data Source=.;Initial Catalog=Products;Integrated Security=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Product>().Configs();
            modelBuilder.Entity<Comment>().Configs();
        }

        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Comment> Posts { get; set; }
    }
}
