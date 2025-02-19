using Microsoft.EntityFrameworkCore;
using WatchLk.ProductProcessing.Domains.Models;

namespace WatchLk.ProductProcessing.Infrastructure
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Product>()
                .Property(x => x.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Brand>().HasData
                (
                    new { Id = 1, Name = "Rolex" },
                    new { Id = 2, Name = "Omega" },
                    new { Id = 3, Name = "IWC Schaffhausen" },
                    new { Id = 4, Name = "Timex" },
                    new { Id = 5, Name = "Citizen" },
                    new { Id = 6, Name = "Apple" },
                    new { Id = 7, Name = "Samsung" },
                    new { Id = 8, Name = "Huawei" },
                    new { Id = 9, Name = "Apple" }
                );

            modelBuilder.Entity<Category>().HasData
                (
                    new { Id = 1, Name = "Uncategorized" },
                    new { Id = 2, Name = "Men" },
                    new { Id = 3, Name = "Women" },
                    new { Id = 4, Name = "Kids" },
                    new { Id = 5, Name = "Smart" }
                );
        }
    }
}
