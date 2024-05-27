using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLogic.API;
using TourPlanner.BusinessLogic.API.Models;
using TourPlanner.DataLayer;
using TourPlanner.Models;

namespace TourPlanner.BusinessLogic
{
    public class BLHandler
    {
        private DLHandler _dlHandler;
        private APIRequestDirections _apiHandler;
        public BLHandler(DLHandler dlHandler, APIRequestDirections apiHandler)
        {
            _dlHandler = dlHandler;
            _apiHandler = apiHandler;
        }

        public async Task<ResponseDirectionsModel> GetTourDetails(string startAdress, string destinationAdress, string transportType)
        {
            return await _apiHandler.GetDirections(startAdress, destinationAdress, transportType);
        }

        public bool SaveTourDb(Tour newTour)
        {
            return _dlHandler.SaveTourToDb(ModelConverter.ConvertOnlyTourToDbModelTour(newTour));
        }

        public DLHandler DLHandler => _dlHandler;
        public APIRequestDirections ApiHandler => _apiHandler;
    }
}
