using Microsoft.EntityFrameworkCore;
using SneakerSZN_BLL.Models;

namespace SneakerSZN_DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        { 
        }

        public DbSet<Sneaker> Sneakers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sneaker>().ToTable("Sneakers");
            modelBuilder.Entity<Sneaker>().HasKey(s => s.Id);
        }
    }
}
