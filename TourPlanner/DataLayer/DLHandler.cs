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
    }
}
