// Controllers/CarsController.cs
using AutoMapper;  // Adăugat pentru AutoMapper (using necesar pentru IMapper)
using livescout_backend.Models;
using livescout_backend.Data;
using livescout_backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LiveScout.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;  // Adăugat pentru AutoMapper: Declară IMapper

        public CarsController(AppDbContext context, IMapper mapper)  // Adăugat pentru AutoMapper: Injectează IMapper în constructor
        {
            _context = context;
            _mapper = mapper;  // Adăugat pentru AutoMapper: Inițializează _mapper
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarListDto>>> Get(
            [FromQuery] int? year,
            [FromQuery] string? brand,
            [FromQuery] string? model,
            [FromQuery] string? bodyType,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int? maxMileage,
            [FromQuery] string? fuelType,
            [FromQuery] string? transmission,
            [FromQuery] string? color,
            [FromQuery] bool? isAvailable
        )
        {
            var query = _context.Cars.AsQueryable();

            // Aplică filtrele (neschimbat, dar adaugă paginare în viitor: query = query.Skip(skip).Take(take);)

            if (year.HasValue) query = query.Where(c => c.Year == year.Value);
            if (!string.IsNullOrEmpty(brand)) query = query.Where(c => c.Brand.Contains(brand));
            if (!string.IsNullOrEmpty(model)) query = query.Where(c => c.Model.Contains(model));
            if (!string.IsNullOrEmpty(bodyType)) query = query.Where(c => c.BodyType == bodyType);
            if (minPrice.HasValue) query = query.Where(c => c.Price >= minPrice.Value);
            if (maxPrice.HasValue) query = query.Where(c => c.Price <= maxPrice.Value);
            if (maxMileage.HasValue) query = query.Where(c => c.Mileage <= maxMileage.Value);
            if (!string.IsNullOrEmpty(fuelType)) query = query.Where(c => c.FuelType == fuelType);
            if (!string.IsNullOrEmpty(transmission)) query = query.Where(c => c.Transmission == transmission);
            if (!string.IsNullOrEmpty(color)) query = query.Where(c => c.Color.Contains(color));
            if (isAvailable.HasValue) query = query.Where(c => c.IsAvailable == isAvailable.Value);

            var cars = await query.ToListAsync();

            // Adăugat pentru AutoMapper: Înlocuiește maparea manuală cu _mapper.Map
            var dtos = _mapper.Map<IEnumerable<CarListDto>>(cars);

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDetailsDto>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            // Adăugat pentru AutoMapper: Înlocuiește maparea manuală cu _mapper.Map
            var dto = _mapper.Map<CarDetailsDto>(car);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<CarDetailsDto>> PostCar(CreateCarDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Adăugat pentru AutoMapper: Mapare din DTO la entity cu _mapper.Map
            var car = _mapper.Map<Car>(dto);

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            // Adăugat pentru AutoMapper: Mapare pentru return cu _mapper.Map
            var responseDto = _mapper.Map<CarDetailsDto>(car);

            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, UpdateCarDto dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            // Adăugat pentru AutoMapper: Mapare din DTO la entity existent cu _mapper.Map (actualizează proprietățile)
            _mapper.Map(dto, car);

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}