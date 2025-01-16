using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SneakerSZN_BLL.Models;

namespace SneakerSZN_DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        { 
        }

        public DbSet<Sneaker> Sneakers { get; set; }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sneaker>()
                .HasOne(s => s.Brand)
                .WithMany(m => m.Sneakers)
                .HasForeignKey(s => s.BrandId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
