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

namespace TourPlanner.DataLayer
{
    internal class TourDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<TourDbModel> Tours { get; set; }
        public DbSet<TourLogDbModel> TourLogs { get; set; }

        public TourDbContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new[] { new LogInterceptor() });
            optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("TourDbContext"));

        }
    }
}
