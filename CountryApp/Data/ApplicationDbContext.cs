using CountryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CountryApp.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CountryWant> TravelList { get; set; }
    }
}
