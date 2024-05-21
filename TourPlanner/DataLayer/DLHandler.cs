using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataLayer
{
    public class DLHandler
    {
        private DbHandler _dbHandler;
        public DLHandler(DbHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }
    }
}
