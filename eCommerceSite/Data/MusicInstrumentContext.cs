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

        /*
         * If want to something store in database, you need to add DbSet property for that class
         * It tells Entity Framework we need to keep track of these objects in the database
         */
        public DbSet<Member> Members { get; set; }
        
    }
}
