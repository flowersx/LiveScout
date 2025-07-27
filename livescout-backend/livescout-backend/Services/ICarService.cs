// Services/ICarService.cs
using livescout_backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace livescout_backend.Services
{
    public interface ICarService
    {
        Task<IEnumerable<CarListDto>> GetAllCarsAsync(int? year, string? brand, string? model, string? bodyType, decimal? minPrice, decimal? maxPrice, int? maxMileage, string? fuelType, string? transmission, string? color, bool? isAvailable, int page = 1, int pageSize = 10);
        Task<CarDetailsDto?> GetCarByIdAsync(int id);
        Task<CarDetailsDto> CreateCarAsync(CreateCarDto dto);
        Task UpdateCarAsync(int id, UpdateCarDto dto);
        Task DeleteCarAsync(int id);
    }
}