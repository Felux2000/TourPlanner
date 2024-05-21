using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataLayer.Models;

namespace TourPlanner.DataLayer
{
    public class DbHandler
    {
        public DbHandler()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .AddJsonFile("C:\\Users\\Jonas\\Documents\\FhTechnikumGitHub\\Semester4\\SWEN2\\TourPlanner\\TourPlanner\\DataLayer\\dbSettings.json", optional: false, reloadOnChange: true)
                    .Build();

            using(TourDbContext dbContext = new(configuration))
            {
                try
                { 
                    dbContext.Database.EnsureDeleted();
                    dbContext.Database.EnsureCreated();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Make sure you are running a local PostgresDB with the postgis extension installed and username/password and port are correct!");
                    Console.WriteLine("E.g. by using the docker compose file in the root of this repository.");
                    Console.WriteLine("docker compose up -d");
                }
            }
        }
    }
}
