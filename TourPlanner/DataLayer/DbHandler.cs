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
using TourPlanner.DataLayer.Repositories;

namespace TourPlanner.DataLayer
{
    public class DbHandler
    {
        private TourDbContext dbContext;
        private TourRepository _tourRepository;
        private TourLogRepository _tourLogRepository;
        public DbHandler(LogInterceptor logInterceptor)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .AddJsonFile($"{Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\DataLayer\\dbSettings.json", optional: false, reloadOnChange: true)
                    .Build();

            dbContext = new(configuration, logInterceptor);
            //try
            //{ 
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                //Following code if for testing purposes only
                _tourRepository = new TourRepository(dbContext);
                _tourLogRepository = new TourLogRepository(dbContext);
                TourLogDbModel log1 = new TourLogDbModel(DateTime.Now, new TimeSpan(12, 23, 46), 12, "The string representations of TimeSpan values are produced by calls to the overloads of the TimeSpan.ToString method, as well as by methods that support composite formatting, such as String.Format. For more information, see Formatting Types and Composite Formatting. The following example illustrates the use of standard format", 5, 10);
                TourLogDbModel log2 = new TourLogDbModel(DateTime.Now, new TimeSpan(12, 23, 46), 12, "comment of the second log", 5, 10);
                TourDbModel tour = new TourDbModel("Car", "Good for cars to loose the zoomies", "Top of car tree", "food bowl", "car", 2, 0.1f, "/Resources/exampleImage.png");
                tour.Logs.Add(log1);
                tour.Logs.Add(log2);

                _tourRepository.AddTour(tour);
                _tourLogRepository.RemoveTour(log1);
                
                List<TourDbModel> tourList = _tourRepository.GetAllTours().ToList<TourDbModel>();
                List<TourLogDbModel> logList = tourList[0].Logs.ToList<TourLogDbModel>();
                Debug.WriteLine(logList[0].Comment);
            /*}
            catch (Exception ex)
            {
                Console.WriteLine("Make sure you are running a local PostgresDB with the postgis extension installed and username/password and port are correct!");
                Console.WriteLine("E.g. by using the docker compose file in the root of this repository.");
                Console.WriteLine("docker compose up -d");
            }*/
        }
    }
}
