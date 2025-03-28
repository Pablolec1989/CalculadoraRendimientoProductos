using Microsoft.EntityFrameworkCore;
using ProductPerformanceCalculator.Entities;

namespace ProductPerformanceCalculator
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Producto> Productos { get; set; }
    }
}
