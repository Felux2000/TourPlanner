using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataLayer;

namespace TourPlanner.BusinessLogic
{
    public class BLHandler
    {
        private DLHandler _dlHandler;
        public BLHandler(DLHandler dlHandler)
        {
            _dlHandler = dlHandler;
        }
    }
}
