using CachedServiceDemo.Entites;
using Microsoft.EntityFrameworkCore;

namespace CachedServiceDemo.Context
{
    public class CachedServiceDemoDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=Berkeyld.44;Host=localhost;Port=5432;Database=CachedServiceDemoDB;");
        }
    }
}
