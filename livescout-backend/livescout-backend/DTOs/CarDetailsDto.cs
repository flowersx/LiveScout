namespace livescout_backend.DTOs
{
    public class CarDetailsDto
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string BodyType { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public string? ImageUrl { get; set; }

        public string DisplayName => $"{Brand} {Model} ({Year})";
        public string PriceFormatted => $"{Price:C0}";
        public string MileageFormatted => $"{Mileage:N0} km";
        public int Age => DateTime.Now.Year - Year;
    }
}