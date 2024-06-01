using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataLayer.Models;

namespace TourPlanner.DataLayer
{
    public class DLHandler
    {
        private DbHandler _dbHandler;

        public DLHandler(DbHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }

        public void SaveTourToDb(TourDbModel newTour)
        {
            _dbHandler.AddTour(newTour);
        }

        public void UpdateTourInDb(TourDbModel updatedTour)
        {
            _dbHandler.UpdateTour(updatedTour);
        }

        public void UpdateTourLogInDb(TourLogDbModel updatedLog)
        {
            _dbHandler.UpdateTourLog(updatedLog);
        }

        public void DeleteTourFromDb(TourDbModel removeableTour)
        {
            _dbHandler.DeleteTour(removeableTour);
        }
        public void DeleteTourLogFromDb(TourLogDbModel removeableLog)
        {
            _dbHandler.DeleteLog(removeableLog);
        }

        public ICollection<TourDbModel> LoadToursFromDb()
        {
            return _dbHandler.GetAllTours();
        }

        public void SaveTourLogToDb(TourDbModel relatedTour, TourLogDbModel newTourLog)
        {
            _dbHandler.AddTourLog(relatedTour, newTourLog);
        }
    }
}
