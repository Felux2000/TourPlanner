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

        public bool SaveTourToDb(TourDbModel newTour)
        {
            return _dbHandler.AddTour(newTour);
        }

        public bool UpdateTourInDb(TourDbModel updatedTour)
        {
            return _dbHandler.UpdateTour(updatedTour);
        }

        public bool UpdateTourLogInDb(TourLogDbModel updatedLog)
        {
            return _dbHandler.UpdateTourLog(updatedLog);
        }

        public ICollection<TourDbModel> LoadToursFromDb()
        {
            return _dbHandler.GetAllTours();
        }

        public bool SaveTourLogToDb(TourDbModel relatedTour, TourLogDbModel newTourLog)
        {
            return _dbHandler.AddTourLog(relatedTour, newTourLog);
        }
    }
}
