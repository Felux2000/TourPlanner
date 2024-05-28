using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
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

        public async Task<(string,ResponseDirectionsModel)> GetTourDetails(string startAdress, string destinationAdress, string transportType)
        {
            return await _apiHandler.GetDirections(startAdress, destinationAdress, transportType);
        }

        public bool SaveTourDb(Tour newTour)
        {
            return _dlHandler.SaveTourToDb(ModelConverter.ConvertSingleTourToDbModelTour(newTour));
        }

        public bool SaveTourLogDb(Tour relatedTour, TourLog newTourLog)
        {
            return _dlHandler.SaveTourLogToDb(ModelConverter.ConvertSingleTourToDbModelTour(relatedTour), ModelConverter.ConvertTourLogToDbModelTour(newTourLog));
        }

        public bool UpdateTourDb(Tour updatedTour)
        {
            return _dlHandler.UpdateTourInDb(ModelConverter.ConvertSingleTourToDbModelTour(updatedTour));
        }

        public bool UpdateTourLogDb(TourLog updatedLog)
        {
            return _dlHandler.UpdateTourLogInDb(ModelConverter.ConvertTourLogToDbModelTour(updatedLog));
        }

        public bool DeleteTourDb(Tour removeableTour)
        {
            return _dlHandler.DeleteTourFromDb(ModelConverter.ConvertSingleTourToDbModelTour(removeableTour));
        }
        public bool DeleteTourLogDb(TourLog removeableTourLog)
        {
            return _dlHandler.DeleteTourLogFromDb(ModelConverter.ConvertTourLogToDbModelTour(removeableTourLog));
        }

        public List<Tour> LoadToursDb()
        {
            return ModelConverter.ConvertListTourDbModelToTour(_dlHandler.LoadToursFromDb());
        }

        public DLHandler DLHandler => _dlHandler;
        public APIRequestDirections ApiHandler => _apiHandler;
    }
}
