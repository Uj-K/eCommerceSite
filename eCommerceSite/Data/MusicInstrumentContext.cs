using eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace eCommerceSite.Data
{
    public class MusicInstrumentContext : DbContext
    {
        public MusicInstrumentContext(DbContextOptions<MusicInstrumentContext> options) : base(options) 
        { 
        }

        public DbSet<MusicInstrument> MusicInstruments { get; set; }
    }
}
