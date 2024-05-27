using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataLayer.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TourPlanner.DataLayer.Repositories
{
    public class TourRepository
    {
        private readonly TourDbContext context;
        public TourRepository(TourDbContext context)
        {
            this.context = context;
        }

        public void AddTour(TourDbModel tour)
        {
            context.Add(tour);
            context.SaveChanges();
        }

        public ICollection<TourDbModel> GetAllTours()
        {
            return context.Tours.Include(p => p.Logs).OrderBy(p => p.Name).ToList();
        }
        public void UpdateTour(TourDbModel tour)
        {
            var entity = context.Tours.Find(tour.Id);
            if (entity == null)
            {
                throw new ArgumentOutOfRangeException("tour", "No tour found with that id to update!");
            }

            context.Entry(entity).CurrentValues.SetValues(tour);
            context.SaveChanges();
        }

        public void RemoveTour(TourDbModel tour)
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
