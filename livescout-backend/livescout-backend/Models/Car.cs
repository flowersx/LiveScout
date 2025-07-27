// Models/Car.cs
using System.ComponentModel.DataAnnotations;

namespace livescout_backend.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Brand { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string Model { get; set; } = null!;
        [Required]
        [Range(1900, 2100)]
        public int Year { get; set; }
        [Required]
        [MaxLength(20)]
        public string BodyType { get; set; } = null!;
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Mileage { get; set; }
        [Required]
        [MaxLength(20)]
        public string FuelType { get; set; } = null!;
        [Required]
        [MaxLength(20)]
        public string Transmission { get; set; } = null!;
        [Required]
        [MaxLength(30)]
        public string Color { get; set; } = null!;
        public bool IsAvailable { get; set; } = true;
        public string? ImageUrl { get; set; }
    }
}