using Microsoft.EntityFrameworkCore;
using TourPlanner.DataLayer.Exceptions;
using TourPlanner.DataLayer.Models;
using TourPlanner.HelperLayer.Logger;

namespace TourPlanner.DataLayer.Repositories
{
    public class TourRepository
    {
        private readonly TourDbContext context;
        private static readonly ILoggerWrapper logger = LoggerFactory.GetLogger();
        public TourRepository(TourDbContext context)
        {
            this.context = context;
        }

        public void AddTour(TourDbModel tour)
        {
            try
            {
                context.Add(tour);
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var dlEx = new DLInvalidEntityException("Invalid entity contents!", ex);
                throw dlEx;
            }
        }

        public ICollection<TourDbModel> GetAllTours()
        {
            return context.Tours.Include(p => p.Logs).OrderBy(p => p.Name).ToList();
        }
        public void UpdateTour(TourDbModel tour)
        {
            try
            {
                var entity = context.Tours.Find(tour.Id);
                if (entity == null)
                {
                    var ex = new DLEntityNotFoundException("Tour to update not found!");
                    logger.Error($"{ex.Message} Tour with Id={tour.Id} does not exist in Database");
                    throw ex;
                }
                context.Entry(entity).CurrentValues.SetValues(tour);
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var dlEx = new DLInvalidEntityException("Invalid entity contents!", ex);
                throw dlEx;
            }
        }

        public void DeleteTour(TourDbModel tour)
        {
            var c = context.Tours.Find(tour.Id);
            if (c != null)
            {
                context.Tours.Remove(c);
                context.SaveChanges();
            }
        }
    }
}
