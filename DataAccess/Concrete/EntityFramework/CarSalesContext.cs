using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class CarSalesContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=CarSalesDB;Username=postgres;Password=eMRE999");
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
} 