using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TourPlanner.DataLayer.Models;

namespace TourPlanner.DataLayer
{
    public class TourDbContext : DbContext
    {
        public IConfiguration Configuration;
        public DbSet<TourDbModel> Tours { get; set; }
        public DbSet<TourLogDbModel> TourLogs { get; set; }

        public TourDbContext(string settingsPath = null)
        {
            const string defaultDir = "./dbSettings.json";
            Configuration = new ConfigurationBuilder()
                    .AddJsonFile($"{settingsPath ?? defaultDir}", optional: false, reloadOnChange: true)
                    .Build();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("TourDbContext"));

        }
    }
}
