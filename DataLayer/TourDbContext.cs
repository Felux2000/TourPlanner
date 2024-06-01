﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TourPlanner.DataLayer.Models;
using System.Diagnostics;
using System.IO;

namespace TourPlanner.DataLayer
{
    public class TourDbContext : DbContext
    {
        public IConfiguration Configuration;

        private LogInterceptor _interceptor;
        public DbSet<TourDbModel> Tours { get; set; }
        public DbSet<TourLogDbModel> TourLogs { get; set; }

        public TourDbContext(LogInterceptor interceptor, string settingsPath = null)
        {
            const string defaultDir = "./dbSettings.json";
            Configuration = new ConfigurationBuilder()
                    .AddJsonFile($"{settingsPath ?? defaultDir}", optional: false, reloadOnChange: true)
                    .Build();
            _interceptor = interceptor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new[] { _interceptor });
            optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("TourDbContext"));

        }
    }
}