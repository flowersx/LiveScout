
using Microsoft.EntityFrameworkCore;
using livescout_backend.Models;

namespace livescout_backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Car>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);

            
            modelBuilder.Entity<Car>()
                .Property(c => c.Brand)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .Property(c => c.Model)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .Property(c => c.BodyType)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .Property(c => c.FuelType)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .Property(c => c.Transmission)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .Property(c => c.Color)
                .HasMaxLength(30)
                .IsRequired();

            
            modelBuilder.Entity<Car>()
                .Property(c => c.ImageUrl);  

            
            modelBuilder.Entity<Car>().HasIndex(c => c.Year);
            modelBuilder.Entity<Car>().HasIndex(c => c.Price);
            modelBuilder.Entity<Car>().HasIndex(c => c.Brand);

            
            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    Id = 1,
                    Brand = "BMW",
                    Model = "Seria 3",
                    Year = 2020,
                    BodyType = "SEDAN",
                    Price = 25000,
                    Mileage = 50000,
                    FuelType = "Benzină",
                    Transmission = "Automată",
                    Color = "Negru",
                    IsAvailable = true,
                    ImageUrl = "https://via.placeholder.com/300x200?text=BMW+Seria+3"  
                },
                new Car
                {
                    Id = 2,
                    Brand = "Audi",
                    Model = "A4",
                    Year = 2018,
                    BodyType = "SEDAN",
                    Price = 20000,
                    Mileage = 80000,
                    FuelType = "Diesel",
                    Transmission = "Manuală",
                    Color = "Alb",
                    IsAvailable = true,
                    ImageUrl = "https://via.placeholder.com/300x200?text=Audi+A4"  
                }
            );
        }
    }
}