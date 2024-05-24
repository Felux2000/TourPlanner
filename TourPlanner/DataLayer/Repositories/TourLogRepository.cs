using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataLayer.Models;

namespace TourPlanner.DataLayer.Repositories
{
    public class TourLogRepository
    {
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
                throw new ArgumentOutOfRangeException("company", "No company found with that id to update!");
            }
            context.Add(newLog);
            entity.Logs.Add(newLog);
            context.SaveChanges();
        }
    }
}
