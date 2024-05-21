using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TourPlanner.DataLayer.Models;

namespace TourPlanner.DataLayer
{
    public class DbHandler
    {
        public DbHandler(LogInterceptor logInterceptor)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .AddJsonFile($"{Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\DataLayer\\dbSettings.json", optional: false, reloadOnChange: true)
                    .Build();

            using(TourDbContext dbContext = new(configuration,logInterceptor))
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
