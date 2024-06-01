using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataLayer.Models;
using TourPlanner.DataLayer.Repositories;

namespace TourPlanner.DataLayer
{
    public class DbHandler
    {
        private TourDbContext _dbContext;
        private TourRepository _tourRepository;
        private TourLogRepository _tourLogRepository;
        public DbHandler(TourDbContext dbContext, TourRepository tourRepository, TourLogRepository tourLogRepository)
        {
            _dbContext = dbContext;
            _tourRepository = tourRepository;
            _tourLogRepository = tourLogRepository;

            //try
            //{ 
            //_dbContext.Database.EnsureDeleted();
            //_dbContext.Database.EnsureCreated();
            /*}
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }*/
        }

        public void AddTour(TourDbModel newTour)
        {
            _tourRepository.AddTour(newTour);
        }

        public void UpdateTour(TourDbModel updatedTour)
        {
            _tourRepository.UpdateTour(updatedTour);
        }
        public void DeleteTour(TourDbModel removeableTour)
        {
            _tourRepository.DeleteTour(removeableTour);
        }

        public ICollection<TourDbModel> GetAllTours()
        {
            return _tourRepository.GetAllTours();
        }

        public void AddTourLog(TourDbModel relatedTour, TourLogDbModel newTourLog)
        {
            _tourLogRepository.AddTourLogToTour(relatedTour, newTourLog);
        }
        public void UpdateTourLog(TourLogDbModel updatedTourLog)
        {
            _tourLogRepository.UpdateTourLog(updatedTourLog);
        }
        public void DeleteLog(TourLogDbModel removeableLog)
        {
            _tourLogRepository.DeleteTourLog(removeableLog);
        }
    }
}
