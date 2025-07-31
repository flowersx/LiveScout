using System.ComponentModel.DataAnnotations;

namespace livescout_backend.DTOs
{
    public class UpdateCarDto
    {
        [Required(ErrorMessage = "ID-ul este obligatoriu")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand-ul este obligatoriu")]
        [StringLength(50, ErrorMessage = "Brand-ul nu poate avea mai mult de 50 de caractere")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Modelul este obligatoriu")]
        [StringLength(50, ErrorMessage = "Modelul nu poate avea mai mult de 50 de caractere")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Anul este obligatoriu")]
        [Range(1900, 2030, ErrorMessage = "Anul trebuie să fie între 1900 și 2030")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Tipul de caroserie este obligatoriu")]
        [StringLength(20, ErrorMessage = "Tipul de caroserie nu poate avea mai mult de 20 de caractere")]
        public string BodyType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prețul este obligatoriu")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Prețul trebuie să fie pozitiv")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Kilometrajul este obligatoriu")]
        [Range(0, int.MaxValue, ErrorMessage = "Kilometrajul nu poate fi negativ")]
        public int Mileage { get; set; }

        [Required(ErrorMessage = "Tipul de combustibil este obligatoriu")]
        [StringLength(20, ErrorMessage = "Tipul de combustibil nu poate avea mai mult de 20 de caractere")]
        public string FuelType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipul de transmisie este obligatoriu")]
        [StringLength(20, ErrorMessage = "Tipul de transmisie nu poate avea mai mult de 20 de caractere")]
        public string Transmission { get; set; } = string.Empty;

        [Required(ErrorMessage = "Culoarea este obligatorie")]
        [StringLength(30, ErrorMessage = "Culoarea nu poate avea mai mult de 30 de caractere")]
        public string Color { get; set; } = string.Empty;

        public string? ImageUrl { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;
    }
}