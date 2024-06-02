using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;
using TourPlanner.DataLayer.Exceptions;
using TourPlanner.DataLayer.Models;
using TourPlanner.HelperLayer.Logger;

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
            try
            {
                var entity = context.Tours
                    .Include(p => p.Logs)
                    .FirstOrDefault(p => p.Id == tour.Id);
                if (entity == null)
                {
                    var ex = new DLEntityNotFoundException("Tour to add log not found!");
                    logger.Error($"{ex.Message} Tour with Id={tour.Id} does not exist in Database");
                    throw ex;
                }
                context.Add(newLog);
                entity.Logs.Add(newLog);
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var dlEx = new DLInvalidEntityException("Invalid entity contents!", ex);
                throw dlEx;
            }
        }

        public void UpdateTourLog(TourLogDbModel log)
        {
            try
            {
                var entity = context.TourLogs.Find(log.Id);
                if (entity == null)
                {
                    var ex = new DLEntityNotFoundException("Tourlog to update not found!");
                    logger.Error($"{ex.Message} Tourlog with Id={log.Id} does not exist in Database");
                    throw ex;
                }
                context.Entry(entity).CurrentValues.SetValues(log);
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var dlEx = new DLInvalidEntityException("Invalid entity contents!", ex);
                throw dlEx;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
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
