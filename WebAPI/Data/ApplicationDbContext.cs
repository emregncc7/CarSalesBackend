using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using Core.Entities.Concrete;

namespace WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<TestDrive> TestDrives { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<CarImage> CarImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Car entity configuration
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Brand).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Model).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Year).IsRequired();
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Description).HasMaxLength(2000);
                entity.HasMany(e => e.Images).WithOne(e => e.Car).HasForeignKey(e => e.CarId);
            });

            // User entity configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.PasswordSalt).IsRequired();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
                entity.Property(e => e.IsActive).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Customer entity configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
                entity.HasMany(e => e.TestDrives).WithOne(e => e.Customer).HasForeignKey(e => e.CustomerId);
                entity.HasMany(e => e.Inquiries).WithOne(e => e.Customer).HasForeignKey(e => e.CustomerId);
            });

            // TestDrive entity configuration
            modelBuilder.Entity<TestDrive>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ScheduledDate).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.HasOne(e => e.Car).WithMany(e => e.TestDrives).HasForeignKey(e => e.CarId);
            });

            // Inquiry entity configuration
            modelBuilder.Entity<Inquiry>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Subject).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(2000);
                entity.Property(e => e.Status).IsRequired();
                entity.HasOne(e => e.Car).WithMany(e => e.Inquiries).HasForeignKey(e => e.CarId);
            });

            // CarImage entity configuration
            modelBuilder.Entity<CarImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ImageUrl).IsRequired().HasMaxLength(500);
                entity.Property(e => e.IsPrimary).IsRequired();
            });
        }
    }
} 