using System.Collections.ObjectModel;
using TourPlanner.DataLayer.Models;
using TourPlanner.HelperLayer.Models;

namespace TourPlanner.BusinessLayer
{
    public static class ModelConverter
    {
        public static List<Tour> ConvertListTourDbModelToTour(ICollection<TourDbModel> dbTours)
        {
            List<Tour> convertedTourList = new List<Tour>();
            List<TourDbModel> tourDbModelList = dbTours.ToList();
            foreach (TourDbModel dbTour in tourDbModelList)
            {
                Tour newTour = new Tour(
                    dbTour.Id,
                    dbTour.Name,
                    dbTour.Description,
                    dbTour.From,
                    dbTour.To,
                    dbTour.TransportType,
                    dbTour.Distance,
                    dbTour.Estimation,
                    dbTour.MapJson
                    );

                List<TourLog> convertedLogList = new List<TourLog>();
                List<TourLogDbModel> logDbList = dbTour.Logs.OrderByDescending(l => l.Date).ToList();
                foreach (TourLogDbModel dbLog in logDbList)
                {
                    convertedLogList.Add(new TourLog(
                        dbLog.Id,
                        dbLog.Date.UtcDateTime,
                        dbLog.Duration,
                        dbLog.Distance,
                        dbLog.Comment,
                        dbLog.Difficulty,
                        dbLog.Rating
                        ));
                }
                newTour.LogList = new ObservableCollection<TourLog>(convertedLogList);
                convertedTourList.Add(newTour);
            }
            return convertedTourList;
        }

        public static TourLogDbModel ConvertTourLogToDbModelTour(TourLog tourLog)
        {
            TourLogDbModel convertedTourLog = new TourLogDbModel(tourLog.Id, tourLog.Date, tourLog.Duration, tourLog.Distance, tourLog.Comment, tourLog.Difficulty, tourLog.Rating);
            return convertedTourLog;
        }

        public static TourDbModel ConvertSingleTourToDbModelTour(Tour tour)
        {
            TourDbModel convertedTour = new TourDbModel(tour.Id, tour.Name, tour.Description, tour.From, tour.To, tour.TransportType, tour.Distance, tour.Estimation, tour.MapJson);
            foreach (TourLog log in tour.LogList)
            {
                TourLogDbModel convertedLog = new TourLogDbModel(
                    log.Id,
                    log.Date,
                    log.Duration,
                    log.Distance,
                    log.Comment,
                    log.Difficulty,
                    log.Rating
                    );
                convertedTour.Logs.Add(convertedLog);
            }
            return convertedTour;
        }
    }
}
