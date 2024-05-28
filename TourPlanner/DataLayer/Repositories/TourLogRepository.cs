using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataLayer.Models;
using TourPlanner.Models;
using TourPlanner.logging;

namespace TourPlanner.DataLayer.Repositories
{
    public class TourLogRepository
    {
        private static readonly ILoggerWrapper logger = LoggerFactory.GetLogger();
        private readonly TourDbContext context;
        public TourLogRepository(TourDbContext context)
        {
            this.context = context;
        }

        public void AddTourLogToTour(TourDbModel tour, TourLogDbModel newLog)
        {
            var entity = context.Tours
                .Include(p => p.Logs)
                .FirstOrDefault(p => p.Id == tour.Id);
            if (entity == null)
            {
                logger.Fatal("Failed to add new TourLog to tour in Database. Tour with specified Id does not exist in Database");
                throw new ArgumentOutOfRangeException("company", "No company found with that id to update!");
            }
            context.Add(newLog);
            entity.Logs.Add(newLog);
            context.SaveChanges();
        }

        public void UpdateTourLog(TourLogDbModel log)
        {
            var entity = context.TourLogs.Find(log.Id);
            if (entity == null)
            {
                logger.Fatal("Failed to update new TourLog in Database. TourLog with specified Id does not exist in Database");
                throw new ArgumentOutOfRangeException("log", "No log found with that id to update!");
            }

            context.Entry(entity).CurrentValues.SetValues(log);
            context.SaveChanges();
        }

        public void DeleteTourLog(TourLogDbModel log)
        {
            var c = context.TourLogs.Find(log.Id);
            if (c != null)
            {
                context.TourLogs.Remove(c);
                context.SaveChanges();
            }
        }
    }
}
