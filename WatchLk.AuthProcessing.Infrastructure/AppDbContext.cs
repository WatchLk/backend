using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WatchLk.AuthProcessing.Domains.Models;

namespace WatchLk.AuthProcessing.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminUser = new User
            {
                Id = "1",
                FirstName = "Lahiru",
                LastName = "Nanayakkara",
                UserName = "admin.watchlk@gmail.com",
                Email = "admin.watchlk@gmail.com",
                NormalizedUserName = "admin.watchlk@gmail.com".ToUpper(),
                NormalizedEmail = "admin.watchlk@gmail.com".ToUpper()
            };

            adminUser.PasswordHash = new PasswordHasher<User>().HashPassword(adminUser, "Admin@123");

            builder.Entity<User>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(k => k.UserId);

            builder.Entity<User>()
                .HasData(adminUser);

            var admin = new IdentityRole
            {
                Id = "1",
                Name = "admin",
                NormalizedName = "ADMIN"
            };

            var client = new IdentityRole
            {
                Id = "2",
                Name = "client",
                NormalizedName = "CLIENT"
            };

            builder.Entity<IdentityRole>()
                .HasData(admin, client);

            var adminUserRole = new IdentityUserRole<string>
            {
                RoleId = admin.Id,
                UserId = adminUser.Id
            };

            builder.Entity<IdentityUserRole<string>>()
                .HasData(adminUserRole);
        }
    }
}
