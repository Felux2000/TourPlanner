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
            Debug.WriteLine("code reached");
            context.Add(tour);
            context.SaveChanges();
        }

        public ICollection<TourDbModel> GetAllTours()
        {
            return context.Tours.Include(p=> p.Logs).ToList();
        }

        public void RemoveTour(TourDbModel tour)
        {
            Guid id = tour.Id;
            var c = context.Tours.Find(id);
            if (c!=null)
            {
                context.Tours.Remove(c);
                context.SaveChanges();
            }
        }

        public void UpdateTour(TourDbModel tour)
        {
            var entity = context.Tours.Find(tour.Id);
            if (entity == null)
            {
                throw new ArgumentOutOfRangeException("company", "No company found with that id to update!");
            }

            context.Entry(entity.Logs).CurrentValues.SetValues(tour);
            context.SaveChanges();
        }

    }
}
