// Calea: [NumeProiectBackend]/Controllers/CarsController.cs

using livescout_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace LiveScout.Controllers
{
    [ApiController] // Indică faptul că este un controller de API
    [Route("[controller]")] // Definește ruta de bază pentru acest controller, va fi /Cars
    public class CarsController : ControllerBase
    {
        // O listă statică pentru a simula baza de date.
        // Ulterior, vei înlocui acest lucru cu o interacțiune reală cu SSMS.
        private static List<Car> _cars = new List<Car>
        {
            new Car { Id = 1, Brand = "Audi", Model = "A4", Year = 2020, BodyType = "SEDAN" },
            new Car { Id = 2, Brand = "BMW", Model = "X5", Year = 2022, BodyType = "SUV" },
            new Car { Id = 3, Brand = "Mercedes-Benz", Model = "C-Class Coupe", Year = 2021, BodyType = "COUPE" },
            new Car { Id = 4, Brand = "Volkswagen", Model = "Golf", Year = 2019, BodyType = "HATCHBACK" },
            new Car { Id = 5, Brand = "Audi", Model = "Q7", Year = 2023, BodyType = "SUV" },
            new Car { Id = 6, Brand = "BMW", Model = "Seria 3", Year = 2020, BodyType = "SEDAN" },
            new Car { Id = 7, Brand = "Ford", Model = "Focus", Year = 2018, BodyType = "HATCHBACK" },
            new Car { Id = 8, Brand = "Porsche", Model = "911", Year = 2024, BodyType = "COUPE" },
            new Car { Id = 9, Brand = "Volvo", Model = "XC60", Year = 2022, BodyType = "SUV" }
        };

        [HttpGet] // Acest atribut indică faptul că această metodă răspunde la cereri GET
        public IEnumerable<Car> Get(
            [FromQuery] int? year,       // Parametru opțional pentru filtrarea după An
            [FromQuery] string? brand,   // Parametru opțional pentru filtrarea după Brand
            [FromQuery] string? model,   // Parametru opțional pentru filtrarea după Model
            [FromQuery] string? bodyType // Parametru opțional pentru filtrarea după Tip Caroserie
        )
        {
            var cars = _cars.AsEnumerable(); // Începem cu toate mașinile

            // Aplică filtrele pe baza parametrilor primiți
            if (year.HasValue)
            {
                cars = cars.Where(c => c.Year == year.Value);
            }

            if (!string.IsNullOrEmpty(brand))
            {
                // Folosim Contains și StringComparison.OrdinalIgnoreCase pentru căutare case-insensitive
                cars = cars.Where(c => c.Brand.Contains(brand, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(model))
            {
                cars = cars.Where(c => c.Model.Contains(model, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(bodyType))
            {
                cars = cars.Where(c => c.BodyType.Equals(bodyType, StringComparison.OrdinalIgnoreCase));
            }

            return cars.ToList(); // Returnează lista filtrată de mașini
        }
    }
}