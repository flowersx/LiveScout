using Microsoft.EntityFrameworkCore;
using livescout_backend.Models;

namespace livescout_backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
    }
}
