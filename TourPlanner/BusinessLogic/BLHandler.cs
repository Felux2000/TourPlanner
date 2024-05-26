using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLogic.API;
using TourPlanner.DataLayer;

namespace TourPlanner.BusinessLogic
{
    public class BLHandler
    {
        private DLHandler _dlHandler;
        private APIRequestDirections _apiHandler;
        public BLHandler(DLHandler dlHandler,APIRequestDirections apiHandler)
        {
            _dlHandler = dlHandler;
            _apiHandler = apiHandler;
        }

        public DLHandler DLHandler => _dlHandler;
        public APIRequestDirections ApiHandler => _apiHandler;
    }
}
