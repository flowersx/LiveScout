// Services/CarService.cs
using AutoMapper;  // Dacă folosești AutoMapper (altfel șterge)
using livescout_backend.Data;
using livescout_backend.Models;
using livescout_backend.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace livescout_backend.Services
{
    public class CarService : ICarService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;  // Dacă folosești AutoMapper

        public CarService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarListDto>> GetAllCarsAsync(int? year, string? brand, string? model, string? bodyType, decimal? minPrice, decimal? maxPrice, int? maxMileage, string? fuelType, string? transmission, string? color, bool? isAvailable, int page = 1, int pageSize = 10)
        {
            var query = _context.Cars.AsQueryable();
            // ... aplică filtrele (codul tău existent)

            var cars = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return _mapper.Map<IEnumerable<CarListDto>>(cars);  // Sau mapare manuală dacă fără AutoMapper
        }

        public async Task<CarDetailsDto?> GetCarByIdAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return null;
            return _mapper.Map<CarDetailsDto>(car);  // Sau mapare manuală
        }

        // Metoda lipsă: Implementare pentru CreateCarAsync
        public async Task<CarDetailsDto> CreateCarAsync(CreateCarDto dto)
        {
            var car = _mapper.Map<Car>(dto);  // Mapare din DTO la entity (sau manual: var car = new Car { Brand = dto.Brand, ... };)

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return _mapper.Map<CarDetailsDto>(car);  // Returnează DTO mapat (sau manual)
        }

        public async Task UpdateCarAsync(int id, UpdateCarDto dto)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) throw new Exception("Not found");

            _mapper.Map(dto, car);  // Actualizează entity cu DTO (sau manual)
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) throw new Exception("Not found");

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }
    }
}