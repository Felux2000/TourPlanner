using iText.Commons.Actions.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TourPlanner.Models;
using TourPlanner.DataLayer.Models;
using System.Diagnostics;
using System.IO;

namespace TourPlanner.DataLayer
{
    public class TourDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        private LogInterceptor _interceptor;
        public DbSet<TourDbModel> Tours { get; set; }
        public DbSet<TourLogDbModel> TourLogs { get; set; }

        public TourDbContext(LogInterceptor interceptor)
        {
            _configuration = new ConfigurationBuilder()
                    .AddJsonFile($"{Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\DataLayer\\dbSettings.json", optional: false, reloadOnChange: true)
                    .Build();
            _interceptor = interceptor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new[] { _interceptor });
            optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("TourDbContext"));

        }
    }
}
